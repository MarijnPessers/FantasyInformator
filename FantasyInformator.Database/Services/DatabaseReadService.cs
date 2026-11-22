using System.Collections.Concurrent;
using System.Reflection;
using FantasyInformator.Database.Context;
using FantasyInformator.Database.Mappers;
using FantasyInformator.Database.Models;
using FantasyInformator.Domain;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Services;

internal interface IDatabaseReadService
{
    /// <summary>
    /// Reads everything from the table of a model and returns it as models. Linked data is not read.
    /// Example: <c>ReadAll&lt;Player&gt;()</c> gives all Players, without the team of each player.
    /// </summary>
    Task<IEnumerable<TModel>> ReadAll<TModel>(CancellationToken cancellationToken)
        where TModel : BaseEntity;
}

/// <summary>
/// Without this class every model would need its own read method, such as ReadAllPlayers, ReadAllLeagues and
/// ReadAllTeams. This class reads any model with one method, <see cref="ReadAll{TModel}"/>.
/// Example: <c>ReadAll&lt;Player&gt;()</c> reads every PlayerDb from the player table and returns them as Players.
/// The steps:
/// <list type="number">
/// <item>
/// The database model of a model is found by adding Db to its name.
/// Example: Player gives PlayerDb.
/// </item>
/// <item>
/// All data is read from that table. Linked data is not read.
/// Example: all PlayerDb, without the team of each player.
/// </item>
/// <item>
/// The mapper copies the database models into models.
/// Example: PlayerMapper copies all PlayerDb into Players.
/// </item>
/// </list>
/// Finding the database model by name is slow, so it is done on the first call and remembered after that in
/// <see cref="ReadAllAsyncMethods"/>.
/// </summary>
internal sealed class DatabaseReadService(IDbContextFactory<FantasyInformatorDbContext> contextFactory) : IDatabaseReadService
{
    /// <summary>
    /// A list of ReadAllAsync methods, one per model.
    /// <see cref="ReadAll{TModel}"/> is called many times for the same model, and creating its
    /// <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> every time would be slow. So each one is created once and kept
    /// here. They are held as a Delegate, which is not type specific.
    /// Example: the <c>ReadAllAsync&lt;Player, PlayerDb&gt;()</c> method.
    /// </summary>
    private static readonly ConcurrentDictionary<Type, Delegate> ReadAllAsyncMethods = new();

    /// <summary>
    /// Read as ReadAll + MethodInfo: the information (MethodInfo) about the method
    /// <see cref="ReadAllAsync{TModel, TDatabaseModel}"/>. This field reads nothing itself.
    /// <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> needs a model and a database model, but the database model is
    /// only found while the program runs, so the code cannot name it.
    /// <see cref="CreateReadAllAsyncMethod{TModel}"/> uses it to set <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> to a model and its database model.
    /// Example: ReadAllAsync set to Player and PlayerDb.
    /// </summary>
    private static readonly MethodInfo ReadAllMethodInfo = typeof(DatabaseReadService)
        .GetMethod(nameof(ReadAllAsync), BindingFlags.Instance | BindingFlags.Public)!;

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> ReadAll<TModel>(CancellationToken cancellationToken)
        where TModel : BaseEntity
    {
        // ReadAll works for any model, but the ReadAllAsync that runs is made for one model and its database model. So
        // the ReadAllAsync for TModel is taken from the field ReadAllAsyncMethods, or created with the method
        // CreateReadAllAsyncMethod when the field has none yet. The field gives back a Delegate, which is not type
        // specific, so it is cast to the Func for TModel. ReadAllAsync is a method of this service, so the Func takes the
        // service as its first input, and that is why this is passed first.
        // Example: for Player, the Delegate is cast to the Func that reads all Players, and then it runs with this.
        var readAll = (Func<DatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>>)ReadAllAsyncMethods
            .GetOrAdd(typeof(TModel), _ => CreateReadAllAsyncMethod<TModel>());

        return readAll(this, cancellationToken);
    }

    /// <summary>
    /// Does the actual reading. It needs the model and its database model, so it only runs through the Func that
    /// <see cref="CreateReadAllAsyncMethod{TModel}"/> makes. It reads all data from the table and copies it into models
    /// with its mapper. It is public only so <see cref="ReadAllMethodInfo"/> can find it without reading private
    /// methods; the class is internal, so nothing outside this project can call it.
    /// Example: set to Player and PlayerDb, it reads all PlayerDb and returns them as Players.
    /// </summary>
    public async Task<IEnumerable<TModel>> ReadAllAsync<TModel, TDatabaseModel>(CancellationToken cancellationToken)
        where TModel : BaseEntity
        where TDatabaseModel : BaseEntityDb
    {
        // Step 1: read all data from the table.
        // Example: all PlayerDb.
        var list = await GetAllAsync<TDatabaseModel>(cancellationToken);

        // Step 2: find the mapper that copies the table data into models.
        // Example: PlayerMapper, for PlayerDb into Player.
        var mapper = MapperLookup.Find<TModel, TDatabaseModel>();

        // Step 3: the mapper copies the database models into models.
        // Example: all PlayerDb become Players.
        return mapper.Map(list);
    }

    /// <summary>
    /// <see cref="ReadAll{TModel}"/> only knows the model, but <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> also
    /// needs its database model, which is only found while the program runs. So this method finds the database model
    /// with <see cref="DatabaseModelTypes.Find"/>. Then it takes the field <see cref="ReadAllMethodInfo"/>, which is the same
    /// for every model, and makes from it a <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> set to the model and its
    /// database model. It returns that as a Func for TModel.
    /// Example: for Player it finds PlayerDb and returns the Func that reads all Players.
    /// </summary>
    private static Func<DatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>> CreateReadAllAsyncMethod<TModel>()
        where TModel : BaseEntity =>
        ReadAllMethodInfo
            .MakeGenericMethod(typeof(TModel), DatabaseModelTypes.Find(typeof(TModel)))
            .CreateDelegate<Func<DatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>>>();

    /// <summary>
    /// Reads all data from one table. Linked data is not read; those links stay empty.
    /// Example: all PlayerDb from the player table, without the team each player links to.
    /// </summary>
    private async Task<List<TDatabaseModel>> GetAllAsync<TDatabaseModel>(CancellationToken cancellationToken)
        where TDatabaseModel : BaseEntityDb
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        // EF normally tracks every model it reads, so it can save changes later. This data is only read, never saved
        // back, so AsNoTracking switches the tracking off. That makes reading faster.
        return await context.Set<TDatabaseModel>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
