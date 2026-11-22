using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using FantasyInformator.Database.Context;
using FantasyInformator.Database.Mappers;
using FantasyInformator.Database.Models;
using FantasyInformator.Domain;
using Microsoft.EntityFrameworkCore;
using AppEnums = FantasyInformator.Domain.Enums;

namespace FantasyInformator.Database.Services;

internal interface IDatabaseWriteService
{
    /// <summary>
    /// Saves a list of models to the database in one go. What happens to each model depends on its state:
    /// <see cref="AppEnums.TrackingState.New"/> ones are added, <see cref="AppEnums.TrackingState.Pending"/> ones are
    /// updated, <see cref="AppEnums.TrackingState.Deleted"/> ones are removed and
    /// <see cref="AppEnums.TrackingState.Unchanged"/> ones are skipped. A New model can already be in the database, for
    /// example a player from an earlier Sleeper fetch; it is then updated instead of added a second time. If anything
    /// fails, nothing is saved.
    /// Example: a list of 4 Players, one in each state, gives 1 added, 1 updated, 1 removed and 1 skipped.
    /// </summary>
    Task SaveAll<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
        where TModel : BaseEntity;
}

/// <summary>
/// Without this class every model would need its own save method, such as SavePlayers, SaveLeagues and SaveTeams.
/// This class saves any model with one method, <see cref="SaveAll{TModel}"/>, and finds the database model and the Map
/// method by itself.
/// Example: <c>SaveAll&lt;Player&gt;(players)</c> copies each Player into a PlayerDb and saves it.
/// </summary>
internal sealed class DatabaseWriteService(IDbContextFactory<FantasyInformatorDbContext> contextFactory) : IDatabaseWriteService
{
    /// <summary>
    /// A list of SaveAllAsync methods, one per model.
    /// <see cref="SaveAll{TModel}"/> is called many times for the same model, and creating its
    /// <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> every time would be slow. So each one is created once and kept
    /// here. They are held as a Delegate, which is not type specific.
    /// Example: the <c>SaveAllAsync&lt;Player, PlayerDb&gt;()</c> method.
    /// </summary>
    private static readonly ConcurrentDictionary<Type, Delegate> SaveAllAsyncMethods = new();

    /// <summary>
    /// A list of Map methods, one per pair of model and database model.
    /// Finding a Map method by name is slow, so each one is found once and kept here.
    /// Example: the Map method that copies a Player into a PlayerDb.
    /// </summary>
    private static readonly ConcurrentDictionary<(Type Model, Type Database), Delegate> MapMethods = new();

    /// <summary>
    /// Read as SaveAll + MethodInfo: the information (MethodInfo) about the method
    /// <see cref="SaveAllAsync{TModel, TDatabaseModel}"/>. This field saves nothing itself.
    /// <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> needs a model and a database model, but the database model is
    /// only found while the program runs, so the code cannot name it.
    /// <see cref="CreateSaveAllAsyncMethod{TModel}"/> uses it to set <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> to a model and its database model.
    /// Example: SaveAllAsync set to Player and PlayerDb.
    /// </summary>
    private static readonly MethodInfo SaveAllMethodInfo = typeof(DatabaseWriteService)
        .GetMethod(nameof(SaveAllAsync), BindingFlags.Instance | BindingFlags.Public)!;

    /// <summary>
    /// Read as CreateInListFilter + MethodInfo: the information (MethodInfo) about the method
    /// <see cref="CreateInListFilter{TValue}"/>, the same for every property. This field creates nothing itself.
    /// <see cref="FindDatabaseIdsAsync{TDatabaseModel}"/> uses it to set <see cref="CreateInListFilter{TValue}"/> to the
    /// type of one property of the unique key, because that type is only known while the program runs.
    /// Example: CreateInListFilter set to string, for the property ExternalId.
    /// </summary>
    private static readonly MethodInfo CreateInListFilterMethodInfo = typeof(DatabaseWriteService)
        .GetMethod(nameof(CreateInListFilter), BindingFlags.Static | BindingFlags.Public)!;

    /// <inheritdoc/>
    public Task SaveAll<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
        where TModel : BaseEntity
    {
        // SaveAll works for any model, but the SaveAllAsync that runs is made for one model and its database model. So
        // the SaveAllAsync for TModel is taken from the field SaveAllAsyncMethods, or created with the method
        // CreateSaveAllAsyncMethod when the field has none yet. The field gives back a Delegate, which is not type
        // specific, so it is cast to the Func for TModel. SaveAllAsync is a method of this service, so the Func takes the
        // service as its first input, and that is why this is passed first.
        // Example: for Player, the Delegate is cast to the Func that saves Players, and then it runs with this and the
        // Players.
        var saveAll = (Func<DatabaseWriteService, IEnumerable<TModel>, CancellationToken, Task>)SaveAllAsyncMethods
            .GetOrAdd(typeof(TModel), _ => CreateSaveAllAsyncMethod<TModel>());

        return saveAll(this, models, cancellationToken);
    }

    /// <summary>
    /// <see cref="SaveAll{TModel}"/> only finds this method for a model; this method does the saving. It needs the model
    /// and its database model, so it only runs through the Func that <see cref="CreateSaveAllAsyncMethod{TModel}"/>
    /// makes. It copies each changed model into its database model with the Map method, finds the New ones that are
    /// already in the database with <see cref="FindDatabaseIdsAsync{TDatabaseModel}"/>, marks each one to be added,
    /// updated or removed by its state, and then saves them all at once. It is public only so
    /// <see cref="SaveAllMethodInfo"/> can find it without reading private methods; the class is internal, so nothing
    /// outside this project can call it.
    /// Example: set to Player and PlayerDb, it copies each Player into a PlayerDb and saves it.
    /// </summary>
    public async Task SaveAllAsync<TModel, TDatabaseModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
        where TModel : BaseEntity
        where TDatabaseModel : BaseEntityDb
    {
        var mapper = FindMapMethod<TModel, TDatabaseModel>();
        var savedUtc = DateTime.UtcNow;

        // Unchanged models are never saved, so only the other ones are copied into database models.
        var changes = models
            .Where(x => x.State is not AppEnums.TrackingState.Unchanged)
            .Select(x => (x.State, DatabaseModel: mapper(x)))
            .ToList();

        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        // We save exactly the models in the list. But the copy of a team also carries copies of its players, and by
        // default EF looks inside every model it saves and saves what it finds there too. Switching
        // AutoDetectChangesEnabled off stops EF from looking inside, so only the models in the list are saved.
        // Example: a list with 1 team and its 3 players saves those 4, not also the 3 player copies inside the team.
        context.ChangeTracker.AutoDetectChangesEnabled = false;

        var databaseIds = await FindDatabaseIdsAsync(
            context,
            [.. changes.Where(x => x.State is AppEnums.TrackingState.New).Select(x => x.DatabaseModel)],
            cancellationToken);

        foreach (var (state, databaseModel) in changes)
        {
            // A New model that is already in the database gets the Id of its database entry and is updated, so it is not
            // added a second time.
            // Example: a New Player with ExternalId 1234, while the database has an entry with ExternalId 1234, updates
            // that entry.
            if (databaseIds.TryGetValue(databaseModel, out var databaseId))
            {
                databaseModel.Id = databaseId;
                Stage(context, AppEnums.TrackingState.Pending, databaseModel, savedUtc);
                continue;
            }

            Stage(context, state, databaseModel, savedUtc);
        }

        _ = await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Creates an "in list" filter: it keeps a database entry when its value for one property is in the list.
    /// The query must keep only the database entries whose value for one property is one of the New models' values. This
    /// method builds that part of the query for one property. EF sends each value to the database as its own parameter
    /// instead of writing it into the query. It is public only so <see cref="CreateInListFilterMethodInfo"/> can find it
    /// without reading private methods; the class is internal, so nothing outside this project can call it.
    /// Example: for the property ExternalId and the values 1234 and 5678 it builds "ExternalId is 1234 or 5678".
    /// </summary>
    public static MethodCallExpression CreateInListFilter<TValue>(
        ParameterExpression databaseModel,
        PropertyInfo property,
        IEnumerable<object?> values)
    {
        var list = values.Cast<TValue>().Distinct().ToList();

        // EF only sends the values as parameters when the query reads them from a variable. The Body of the lambda
        // readList reads the variable list, so that Body is used in the query.
        Expression<Func<List<TValue>>> readList = () => list;

        return Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Contains),
            [typeof(TValue)],
            readList.Body,
            Expression.Property(databaseModel, property));
    }

    /// <summary>
    /// <see cref="SaveAll{TModel}"/> only knows the model, but <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> also
    /// needs its database model, which is only found while the program runs. So this method finds the database model
    /// with <see cref="DatabaseModelTypes.Find"/>. Then it takes the field <see cref="SaveAllMethodInfo"/>, which is the
    /// same for every model, and makes from it a <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> set to the model and
    /// its database model. It returns that as a Func for TModel.
    /// Example: for Player it finds PlayerDb and returns the Func that saves Players.
    /// </summary>
    private static Func<DatabaseWriteService, IEnumerable<TModel>, CancellationToken, Task> CreateSaveAllAsyncMethod<TModel>()
        where TModel : BaseEntity =>
        SaveAllMethodInfo
            .MakeGenericMethod(typeof(TModel), DatabaseModelTypes.Find(typeof(TModel)))
            .CreateDelegate<Func<DatabaseWriteService, IEnumerable<TModel>, CancellationToken, Task>>();

    /// <summary>
    /// An updated or deleted model came from the database, so it has the right Id. A New model may still be in the
    /// database already, for example a player from Sleeper that was saved in an earlier fetch. Such a model can only be
    /// recognised by the unique key of its table. So this method reads, in one query, the database entries whose unique
    /// key is among those of the New models, and gives back the Id of the entry for each New model it found. It gives
    /// back nothing when there are no New models or when the table has no unique key. Throws when the table has more
    /// than one unique key.
    /// Example: for 100 New Players it reads the database entries whose ExternalId is one of those 100, and gives back
    /// the Id for each Player that was already there.
    /// </summary>
    private static async Task<Dictionary<TDatabaseModel, Guid>> FindDatabaseIdsAsync<TDatabaseModel>(
        FantasyInformatorDbContext context,
        List<TDatabaseModel> newDatabaseModels,
        CancellationToken cancellationToken)
        where TDatabaseModel : BaseEntityDb
    {
        var databaseIds = new Dictionary<TDatabaseModel, Guid>(ReferenceEqualityComparer.Instance);
        var uniqueKey = context.Model.FindEntityType(typeof(TDatabaseModel))?.GetIndexes().SingleOrDefault(x => x.IsUnique);

        if (newDatabaseModels.Count == 0 || uniqueKey is null)
        {
            return databaseIds;
        }

        var uniqueKeyProperties = uniqueKey.Properties.Select(x => x.PropertyInfo!).ToList();

        // To find which New models are already in the database, the query reads only the entries whose unique key
        // matches one of them, not the whole table.
        // Example: New Players with ExternalId 1234 and 5678 read only the entries with ExternalId 1234 or 5678.
        // A unique key of two properties gets one list per property, and an entry is read when each of its values is in
        // its list. Those values can come from two different New models, so the foreach loop at the end of this method
        // keeps only the entries that match one New model on both values.
        // Example: New (team A, player 1) and (team B, player 2) give the lists (team A, team B) and
        // (player 1, player 2). The entry (team A, player 2) is read, but matches neither New model, so the loop skips it.
        // The unique key is only known while the program runs, so the query is built from one filter per property,
        // joined with AND.
        var databaseModel = Expression.Parameter(typeof(TDatabaseModel), "x");
        var filter = uniqueKeyProperties
            .Select(property => (Expression)CreateInListFilterMethodInfo
                .MakeGenericMethod(property.PropertyType)
                .Invoke(null, [databaseModel, property, newDatabaseModels.Select(x => property.GetValue(x))])!)
            .Aggregate(Expression.AndAlso);

        // Only the Id of each entry is needed, so EF does not track the entries.
        var databaseIdsByUniqueKey = (await context.Set<TDatabaseModel>()
                .AsNoTracking()
                .Where(Expression.Lambda<Func<TDatabaseModel, bool>>(filter, databaseModel))
                .ToListAsync(cancellationToken))
            .ToDictionary(
                x => GetUniqueKeyValues(x, uniqueKeyProperties),
                x => x.Id,
                UniqueKeyValuesComparer.Instance);

        foreach (var newDatabaseModel in newDatabaseModels)
        {
            if (databaseIdsByUniqueKey.TryGetValue(GetUniqueKeyValues(newDatabaseModel, uniqueKeyProperties), out var id))
            {
                databaseIds[newDatabaseModel] = id;
            }
        }

        return databaseIds;
    }

    /// <summary>
    /// The values of the unique key properties of one database model, in the order of the unique key.
    /// Example: for a RosteredPlayerDb, its FantasyTeamId and its PlayerId.
    /// </summary>
    private static object?[] GetUniqueKeyValues(BaseEntityDb databaseModel, List<PropertyInfo> uniqueKeyProperties) =>
        [.. uniqueKeyProperties.Select(x => x.GetValue(databaseModel))];

    /// <summary>
    /// Copying a model into its database model needs the Map method for that pair. It is found once and kept in
    /// <see cref="MapMethods"/>, and cast from Delegate to the Func for this pair. Throws when there is none or more
    /// than one.
    /// Example: the Map method that copies a Player into a PlayerDb.
    /// </summary>
    private static Func<TModel, TDatabaseModel> FindMapMethod<TModel, TDatabaseModel>() =>
        (Func<TModel, TDatabaseModel>)MapMethods.GetOrAdd(
            (typeof(TModel), typeof(TDatabaseModel)),
            _ => MapperLookup
                .Find([typeof(TModel)], typeof(TDatabaseModel))
                .CreateDelegate<Func<TModel, TDatabaseModel>>());

    private static void Stage(
        FantasyInformatorDbContext context,
        AppEnums.TrackingState state,
        BaseEntityDb databaseModel,
        DateTime savedUtc)
    {
        switch (state)
        {
            case AppEnums.TrackingState.New:
                Insert(context, databaseModel, savedUtc);
                break;
            case AppEnums.TrackingState.Pending:
                Update(context, databaseModel, savedUtc);
                break;
            case AppEnums.TrackingState.Deleted:
                Delete(context, databaseModel);
                break;
            case AppEnums.TrackingState.Unchanged:
                break;
            default:
                // Any other state is not saved.
                break;
        }
    }

    private static void Insert(
        FantasyInformatorDbContext context,
        BaseEntityDb databaseModel,
        DateTime savedUtc)
    {
        databaseModel.CreatedUtc = savedUtc;
        databaseModel.UpdatedUtc = savedUtc;
        context.Entry(databaseModel).State = EntityState.Added;
    }

    private static void Update(
        FantasyInformatorDbContext context,
        BaseEntityDb databaseModel,
        DateTime savedUtc)
    {
        databaseModel.UpdatedUtc = savedUtc;

        var entry = context.Entry(databaseModel);
        entry.State = EntityState.Modified;

        // An update writes every column, the created time too. The model sent in has no created time, so that column is
        // left out and the one in the database stays.
        // Example: a Player created on Monday and updated on Friday keeps Monday.
        entry.Property(nameof(BaseEntityDb.CreatedUtc)).IsModified = false;
    }

    private static void Delete(FantasyInformatorDbContext context, BaseEntityDb databaseModel) =>
        context.Entry(databaseModel).State = EntityState.Deleted;

    /// <summary>
    /// Two lists of values are different objects even when they hold the same values, so a dictionary would never find a
    /// unique key by its list. This comparer makes two lists equal when they hold the same values in the same order.
    /// Example: (team A, player 1) equals another (team A, player 1).
    /// </summary>
    private sealed class UniqueKeyValuesComparer : IEqualityComparer<object?[]>
    {
        public static UniqueKeyValuesComparer Instance { get; } = new();

        public bool Equals(object?[]? x, object?[]? y) =>
            ReferenceEquals(x, y) || (x is not null && y is not null && x.SequenceEqual(y));

        public int GetHashCode(object?[] obj)
        {
            var hash = new HashCode();

            foreach (var value in obj)
            {
                hash.Add(value);
            }

            return hash.ToHashCode();
        }
    }
}
