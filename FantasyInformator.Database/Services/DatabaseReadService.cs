using System.Collections.Concurrent;
using System.Reflection;
using FantasyInformator.Database.Context;
using FantasyInformator.Database.Models.Tracking;
using FantasyInformator.Domain.Enums;
using FantasyInformator.Domain.Tracking;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Services;

internal interface IDatabaseReadService
{
    /// <summary>
    /// Reads every row of the database model that belongs to <typeparamref name="TModel"/> (<c>PlayerDb</c> for
    /// <c>Player</c>) and returns the rows as <typeparamref name="TModel"/>. Nothing is enriched: every link to another
    /// model is null, which means it is not retrieved.
    /// </summary>
    Task<IEnumerable<TModel>> ReadAll<TModel>(CancellationToken cancellationToken)
        where TModel : TrackedModel;
}

/// <summary>
/// Reads any model from the database with one call, for example <c>ReadAll&lt;Player&gt;()</c>. The caller only
/// names the model. This class works out the rest while the application runs, by the rules below, so no code
/// is written per model:
/// <list type="number">
/// <item>The database model is the model name with <c>Db</c> behind it (<c>Player</c> gives <c>PlayerDb</c>).</item>
/// <item>All rows of that database model are read. No links to other models are read; they stay null.</item>
/// <item>The rows are turned into models by the <c>Map</c> method that exists for that database model.</item>
/// <item>The id is copied from each row, and the models start as Unchanged, because they match the database.</item>
/// </list>
/// Looking things up by name is slow, so every lookup is done once per model and kept for the next calls.
/// </summary>
internal class DatabaseReadService(IDbContextFactory<FantasyInformatorDbContext> contextFactory) : IDatabaseReadService
{
    private const string DatabaseModelSuffix = "Db";
    private const string MapMethodName = "Map";

    /// <summary>The ready-made read call per model (Player, League, ...), built the first time it is needed.</summary>
    private static readonly ConcurrentDictionary<Type, Delegate> ReadAllCalls = new();

    /// <summary>The ready-made <c>Map</c> call per pair of database model and model, found the first time.</summary>
    private static readonly ConcurrentDictionary<(Type Database, Type Model), Delegate> RowMappers = new();

    /// <summary>
    /// The method <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> with its two types still blank. A method with
    /// blank types cannot be called until the blanks are filled in.
    /// </summary>
    private static readonly MethodInfo ReadAllMethod = typeof(DatabaseReadService)
        .GetMethod(nameof(ReadAllAsync), BindingFlags.Instance | BindingFlags.NonPublic)!;

    public Task<IEnumerable<TModel>> ReadAll<TModel>(CancellationToken cancellationToken)
        where TModel : TrackedModel
    {
        // The read call of a model, such as Player, is built the first time and kept for every next call.
        // The service and the cancellation token are left open: they are filled in on every call.
        // The dictionary can only store a general kind of call, so it is turned back into its real kind here.
        var readAll = (Func<DatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>>)ReadAllCalls
            .GetOrAdd(typeof(TModel), _ => BuildReadAllCall<TModel>());

        return readAll(this, cancellationToken);
    }

    /// <summary>
    /// The database type is only known while running (PlayerDb for Player), so the read method cannot be called
    /// directly. Its two blank types are filled in here, and the result is a call that can be used again.
    /// </summary>
    private static Func<DatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>> BuildReadAllCall<TModel>()
        where TModel : TrackedModel =>
        ReadAllMethod
            .MakeGenericMethod(typeof(TModel), FindDatabaseType(typeof(TModel)))
            .CreateDelegate<Func<DatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>>>();

    /// <summary>
    /// The real read, with both types filled in (for example Player and PlayerDb). It is never called by name:
    /// <see cref="BuildReadAllCall{TModel}"/> fills in the types and <see cref="ReadAll{TModel}"/> calls it.
    /// </summary>
    private async Task<IEnumerable<TModel>> ReadAllAsync<TModel, TDatabaseModel>(CancellationToken cancellationToken)
        where TModel : TrackedModel
        where TDatabaseModel : TrackedModelDb
    {
        // Step 1: read all rows of the database model.
        var rows = await GetAllAsync<TDatabaseModel>(cancellationToken);

        // Step 2: find the Map method that turns these rows into models.
        var mapRows = FindRowMapper<TDatabaseModel, TModel>();

        // The rows come from the database, so every model created while mapping starts as Unchanged.
        using (TrackedModel.SetNewDefaultState(TrackingState.Unchanged))
        {
            // The list is made here, inside the scope. Left lazy, the models would be created after the scope
            // has ended and would start as New.
            var models = mapRows(rows).ToList();

            // Step 3: the mappers never set the id, so it is copied here. Mapping keeps the order of the rows,
            // so the first row belongs to the first model, and so on.
            foreach (var (row, model) in rows.Zip(models))
            {
                model.Id = row.Id;
            }

            return models;
        }
    }

    /// <summary>
    /// Reads every row of one table. <c>Set</c> gives the table of the database model, so no table name is
    /// written. Only the rows themselves are read: links to other tables stay null.
    /// </summary>
    private async Task<List<TDatabaseModel>> GetAllAsync<TDatabaseModel>(CancellationToken cancellationToken)
        where TDatabaseModel : TrackedModelDb
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        // AsNoTracking: the rows are only read, so the context does not need to watch them for changes.
        return await context.Set<TDatabaseModel>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Every model has the database model with the same name and <c>Db</c> behind it. It is searched for in this
    /// project. Throws when there is none or more than one; that is a missing exception to the rule.
    /// </summary>
    private static Type FindDatabaseType(Type modelType) =>
        typeof(TrackedModelDb).Assembly
            .GetTypes()
            .Single(x => x.Name == modelType.Name + DatabaseModelSuffix && x.IsAssignableTo(typeof(TrackedModelDb)));

    /// <summary>
    /// Finds the mapper that turns a list of database models into a list of models: the <c>Map</c> extension method
    /// on <c>IEnumerable&lt;TDatabaseModel&gt;</c> in one of the mapper classes. It looks through all static classes
    /// of this project for a public <c>Map</c> method that takes that list and returns a list of the model.
    /// Throws when there is none or more than one. The method is made into a call once and kept.
    /// </summary>
    private static Func<IEnumerable<TDatabaseModel>, IEnumerable<TModel>> FindRowMapper<TDatabaseModel, TModel>() =>
        (Func<IEnumerable<TDatabaseModel>, IEnumerable<TModel>>)RowMappers.GetOrAdd(
            (typeof(TDatabaseModel), typeof(TModel)),
            _ => typeof(TrackedModelDb).Assembly
                .GetTypes()
                .Where(x => x is { IsAbstract: true, IsSealed: true })
                .SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .Single(x => x.Name == MapMethodName
                    && x.ReturnType == typeof(IEnumerable<TModel>)
                    && x.GetParameters() is [{ } parameter]
                    && parameter.ParameterType == typeof(IEnumerable<TDatabaseModel>))
                .CreateDelegate<Func<IEnumerable<TDatabaseModel>, IEnumerable<TModel>>>());
}
