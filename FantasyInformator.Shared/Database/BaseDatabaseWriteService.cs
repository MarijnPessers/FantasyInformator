using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using FantasyInformator.Shared.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FantasyInformator.Shared.Database;

public interface IDatabaseWriteService
{
    /// <summary>
    /// Saves a list of models to the database in one go. What happens to each model depends on its
    /// <see cref="SaveAction"/>: it is added, updated, removed or skipped. A model to add can already be in the
    /// database, for example one that was saved in an earlier fetch; it is then updated instead of added a second time.
    /// If anything fails, nothing is saved.
    /// Example: a list of 4 teams, one for each action, gives 1 added, 1 updated, 1 removed and 1 skipped.
    /// </summary>
    Task SaveAll<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
        where TModel : class;
}

/// <summary>
/// Without this class every model would need its own save method, such as SaveTeams and SavePlayers. This class saves
/// any model with one method, <see cref="SaveAll{TModel}"/>, and finds the database model and the mapper by itself.
/// What differs per project, the database, the database model of each model, the action for each model and what a
/// saved database model gets besides its fields, is given by the class that derives from this one.
/// Example: saving teams copies each team into a TeamDb and saves it.
/// </summary>
public abstract class BaseDatabaseWriteService : IDatabaseWriteService
{
    /// <summary>
    /// A list of SaveAllAsync methods, one per class that derives from this one and model.
    /// <see cref="SaveAll{TModel}"/> is called many times for the same model, and creating its
    /// <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> every time would be slow. So each one is created once and kept
    /// here. The class that derives from this one is part of the key, because each one gives its own database models.
    /// They are held as a Delegate, which is not type specific.
    /// Example: the <c>SaveAllAsync&lt;Team, TeamDb&gt;()</c> method.
    /// </summary>
    private static readonly ConcurrentDictionary<(Type Service, Type Model), Delegate> SaveAllAsyncMethods = new();

    /// <summary>
    /// Read as SaveAll + MethodInfo: the information (MethodInfo) about the method
    /// <see cref="SaveAllAsync{TModel, TDatabaseModel}"/>. This field saves nothing itself.
    /// <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> needs a model and a database model, but the database model is
    /// only found while the program runs, so the code cannot name it.
    /// <see cref="CreateSaveAllAsyncMethod{TModel}"/> uses it to set <see cref="SaveAllAsync{TModel, TDatabaseModel}"/>
    /// to a model and its database model.
    /// Example: SaveAllAsync set to Team and TeamDb.
    /// </summary>
    private static readonly MethodInfo SaveAllMethodInfo = typeof(BaseDatabaseWriteService)
        .GetMethod(nameof(SaveAllAsync), BindingFlags.Instance | BindingFlags.Public)!;

    /// <summary>
    /// Read as CreateInListFilter + MethodInfo: the information (MethodInfo) about the method
    /// <see cref="CreateInListFilter{TValue}"/>, the same for every property. This field creates nothing itself.
    /// <see cref="FindDatabaseKeysAsync{TDatabaseModel}"/> uses it to set <see cref="CreateInListFilter{TValue}"/> to the
    /// type of one property of the unique key, because that type is only known while the program runs.
    /// Example: CreateInListFilter set to string, for the property ExternalId.
    /// </summary>
    private static readonly MethodInfo CreateInListFilterMethodInfo = typeof(BaseDatabaseWriteService)
        .GetMethod(nameof(CreateInListFilter), BindingFlags.Static | BindingFlags.Public)!;

    /// <inheritdoc/>
    public Task SaveAll<TModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
        where TModel : class
    {
        // SaveAll works for any model, but the SaveAllAsync that runs is made for one model and its database model. So
        // the SaveAllAsync for TModel is taken from the field SaveAllAsyncMethods, or created with the method
        // CreateSaveAllAsyncMethod when the field has none yet. The field gives back a Delegate, which is not type
        // specific, so it is cast to the Func for TModel. SaveAllAsync is a method of this service, so the Func takes the
        // service as its first input, and that is why this is passed first.
        // Example: for Team, the Delegate is cast to the Func that saves teams, and then it runs with this and the teams.
        var saveAll = (Func<BaseDatabaseWriteService, IEnumerable<TModel>, CancellationToken, Task>)SaveAllAsyncMethods
            .GetOrAdd((GetType(), typeof(TModel)), _ => CreateSaveAllAsyncMethod<TModel>());

        return saveAll(this, models, cancellationToken);
    }

    /// <summary>
    /// <see cref="SaveAll{TModel}"/> only finds this method for a model; this method does the saving. It needs the model
    /// and its database model, so it only runs through the Func that <see cref="CreateSaveAllAsyncMethod{TModel}"/>
    /// makes. It copies each model that is not skipped into its database model with its mapper, finds the ones to add
    /// that are already in the database with <see cref="FindDatabaseKeysAsync{TDatabaseModel}"/>, marks each one to be
    /// added, updated or removed by its action, and then saves them all at once. It is public only so
    /// <see cref="SaveAllMethodInfo"/> can find it without reading private methods.
    /// Example: set to Team and TeamDb, it copies each team into a TeamDb and saves it.
    /// </summary>
    public async Task SaveAllAsync<TModel, TDatabaseModel>(IEnumerable<TModel> models, CancellationToken cancellationToken)
        where TModel : class
        where TDatabaseModel : class
    {
        // The mappers are in the project of the class that derives from this one, so the lookup searches the assembly
        // of that class.
        var mapper = MapperLookup.Find<TModel, TDatabaseModel>(GetType().Assembly);
        var savedUtc = DateTime.UtcNow;

        // Skipped models are never saved, so only the other ones are copied into database models.
        var changes = models
            .Select(x => (Action: FindSaveAction(x), Model: x))
            .Where(x => x.Action is not SaveAction.Skip)
            .Select(x => (x.Action, DatabaseModel: mapper.Map(x.Model)))
            .ToList();

        await using var context = await CreateContextAsync(cancellationToken);

        // We save exactly the models in the list. But the copy of a team also carries copies of its players, and by
        // default EF looks inside every model it saves and saves what it finds there too. Switching
        // AutoDetectChangesEnabled off stops EF from looking inside, so only the models in the list are saved.
        // Example: a list with 1 team and its 3 players saves those 4, not also the 3 player copies inside the team.
        context.ChangeTracker.AutoDetectChangesEnabled = false;

        var primaryKeyProperties = FindPrimaryKeyProperties(context, typeof(TDatabaseModel));
        var databaseKeys = await FindDatabaseKeysAsync(
            context,
            [.. changes.Where(x => x.Action is SaveAction.Add).Select(x => x.DatabaseModel)],
            primaryKeyProperties,
            cancellationToken);

        foreach (var (action, databaseModel) in changes)
        {
            // A model to add that is already in the database gets the primary key of its database entry and is
            // updated, so it is not added a second time.
            // Example: a new team with ExternalId 1234, while the database has an entry with ExternalId 1234, updates
            // that entry.
            if (databaseKeys.TryGetValue(databaseModel, out var databaseKey))
            {
                foreach (var (property, value) in primaryKeyProperties.Zip(databaseKey))
                {
                    property.SetValue(databaseModel, value);
                }

                Stage(context, SaveAction.Update, databaseModel, savedUtc);
                continue;
            }

            Stage(context, action, databaseModel, savedUtc);
        }

        _ = await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Creates an "in list" filter: it keeps a database entry when its value for one property is in the list.
    /// The query must keep only the database entries whose value for one property is one of the values of the models to
    /// add. This method builds that part of the query for one property. EF sends each value to the database as its own
    /// parameter instead of writing it into the query. It is public only so <see cref="CreateInListFilterMethodInfo"/>
    /// can find it without reading private methods.
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

    /// <summary>Makes a new context for one save. The caller disposes it.</summary>
    protected abstract Task<DbContext> CreateContextAsync(CancellationToken cancellationToken);

    /// <summary>
    /// The database model of a model. Throws when there is none.
    /// Example: a team gives a TeamDb.
    /// </summary>
    protected abstract Type FindDatabaseModel(Type modelType);

    /// <summary>
    /// What the save does with one model.
    /// Example: a team that was changed gives <see cref="SaveAction.Update"/>.
    /// </summary>
    protected abstract SaveAction FindSaveAction(object model);

    /// <summary>
    /// Runs for each database model that is about to be added, for what it gets besides its own fields.
    /// Example: the time it was created.
    /// </summary>
    protected virtual void OnAdded(EntityEntry entry, DateTime savedUtc)
    {
        // By default an added database model only gets its own fields.
    }

    /// <summary>
    /// Runs for each database model that is about to be updated, for what it gets besides its own fields, or for
    /// columns that must not be written.
    /// Example: the time it was updated.
    /// </summary>
    protected virtual void OnUpdated(EntityEntry entry, DateTime savedUtc)
    {
        // By default an updated database model only gets its own fields.
    }

    /// <summary>
    /// The properties of the primary key of a database model, in the order of the key. Throws when the database model
    /// is not a table of the context.
    /// Example: for a TeamDb, its Id.
    /// </summary>
    private static List<PropertyInfo> FindPrimaryKeyProperties(DbContext context, Type databaseModelType) =>
    [
        .. (context.Model.FindEntityType(databaseModelType)
                ?? throw new InvalidOperationException(
                    $"{databaseModelType.Name} is not a table of {context.GetType().Name}."))
            .FindPrimaryKey()!.Properties.Select(x => x.PropertyInfo!),
    ];

    /// <summary>
    /// An updated or deleted model came from the database, so it has the right primary key. A model to add may still be
    /// in the database already, for example one that was saved in an earlier fetch. Such a model can only be recognised
    /// by the unique key of its table. So this method reads, in one query, the database entries whose unique key is
    /// among those of the models to add, and gives back the primary key of the entry for each model it found. It gives
    /// back nothing when there are no models to add or when the table has no unique key. Throws when the table has more
    /// than one unique key.
    /// Example: for 100 new teams it reads the database entries whose ExternalId is one of those 100, and gives back the
    /// primary key for each team that was already there.
    /// </summary>
    private static async Task<Dictionary<TDatabaseModel, object?[]>> FindDatabaseKeysAsync<TDatabaseModel>(
        DbContext context,
        List<TDatabaseModel> newDatabaseModels,
        List<PropertyInfo> primaryKeyProperties,
        CancellationToken cancellationToken)
        where TDatabaseModel : class
    {
        var databaseKeys = new Dictionary<TDatabaseModel, object?[]>(ReferenceEqualityComparer.Instance);
        var uniqueKey = context.Model.FindEntityType(typeof(TDatabaseModel))?.GetIndexes().SingleOrDefault(x => x.IsUnique);

        if (newDatabaseModels.Count == 0 || uniqueKey is null)
        {
            return databaseKeys;
        }

        var uniqueKeyProperties = uniqueKey.Properties.Select(x => x.PropertyInfo!).ToList();

        // To find which models to add are already in the database, the query reads only the entries whose unique key
        // matches one of them, not the whole table.
        // Example: new teams with ExternalId 1234 and 5678 read only the entries with ExternalId 1234 or 5678.
        // A unique key of two properties gets one list per property, and an entry is read when each of its values is in
        // its list. Those values can come from two different models, so the foreach loop at the end of this method keeps
        // only the entries that match one model on both values.
        // Example: new (team A, player 1) and (team B, player 2) give the lists (team A, team B) and
        // (player 1, player 2). The entry (team A, player 2) is read, but matches neither model, so the loop skips it.
        // The unique key is only known while the program runs, so the query is built from one filter per property,
        // joined with AND.
        var databaseModel = Expression.Parameter(typeof(TDatabaseModel), "x");
        var filter = uniqueKeyProperties
            .Select(property => (Expression)CreateInListFilterMethodInfo
                .MakeGenericMethod(property.PropertyType)
                .Invoke(null, [databaseModel, property, newDatabaseModels.Select(x => property.GetValue(x))])!)
            .Aggregate(Expression.AndAlso);

        // Only the primary key of each entry is needed, so EF does not track the entries.
        var databaseKeysByUniqueKey = (await context.Set<TDatabaseModel>()
                .AsNoTracking()
                .Where(Expression.Lambda<Func<TDatabaseModel, bool>>(filter, databaseModel))
                .ToListAsync(cancellationToken))
            .ToDictionary(
                x => GetPropertyValues(x, uniqueKeyProperties),
                x => GetPropertyValues(x, primaryKeyProperties),
                PropertyValuesComparer.Instance);

        foreach (var newDatabaseModel in newDatabaseModels)
        {
            if (databaseKeysByUniqueKey.TryGetValue(
                GetPropertyValues(newDatabaseModel, uniqueKeyProperties),
                out var databaseKey))
            {
                databaseKeys[newDatabaseModel] = databaseKey;
            }
        }

        return databaseKeys;
    }

    /// <summary>
    /// The values of some properties of one database model, in the order of the properties.
    /// Example: for a RosteredPlayerDb and its unique key, its FantasyTeamId and its PlayerId.
    /// </summary>
    private static object?[] GetPropertyValues(object databaseModel, List<PropertyInfo> properties) =>
        [.. properties.Select(x => x.GetValue(databaseModel))];

    /// <summary>
    /// <see cref="SaveAll{TModel}"/> only knows the model, but <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> also
    /// needs its database model, which is only found while the program runs. So this method finds the database model
    /// with <see cref="FindDatabaseModel"/>. Then it takes the field <see cref="SaveAllMethodInfo"/>, which is the same
    /// for every model, and makes from it a <see cref="SaveAllAsync{TModel, TDatabaseModel}"/> set to the model and its
    /// database model. It returns that as a Func for TModel.
    /// Example: for Team it finds TeamDb and returns the Func that saves teams.
    /// </summary>
    private Func<BaseDatabaseWriteService, IEnumerable<TModel>, CancellationToken, Task> CreateSaveAllAsyncMethod<TModel>()
        where TModel : class =>
        SaveAllMethodInfo
            .MakeGenericMethod(typeof(TModel), FindDatabaseModel(typeof(TModel)))
            .CreateDelegate<Func<BaseDatabaseWriteService, IEnumerable<TModel>, CancellationToken, Task>>();

    private void Stage(DbContext context, SaveAction action, object databaseModel, DateTime savedUtc)
    {
        var entry = context.Entry(databaseModel);

        switch (action)
        {
            case SaveAction.Add:
                entry.State = EntityState.Added;
                OnAdded(entry, savedUtc);
                break;
            case SaveAction.Update:
                entry.State = EntityState.Modified;
                OnUpdated(entry, savedUtc);
                break;
            case SaveAction.Delete:
                entry.State = EntityState.Deleted;
                break;
            case SaveAction.Skip:
                break;
            default:
                // Any other action is not saved.
                break;
        }
    }

    /// <summary>
    /// Two lists of values are different objects even when they hold the same values, so a dictionary would never find a
    /// key by its list. This comparer makes two lists equal when they hold the same values in the same order.
    /// Example: (team A, player 1) equals another (team A, player 1).
    /// </summary>
    private sealed class PropertyValuesComparer : IEqualityComparer<object?[]>
    {
        public static PropertyValuesComparer Instance { get; } = new();

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
