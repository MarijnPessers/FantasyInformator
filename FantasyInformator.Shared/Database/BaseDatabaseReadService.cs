using System.Collections.Concurrent;
using System.Reflection;
using FantasyInformator.Shared.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Shared.Database;

public interface IDatabaseReadService
{
    /// <summary>
    /// Reads everything from the table of a model and returns it as models. Linked data is not read.
    /// Example: reading all teams gives every team, without the players of each team.
    /// </summary>
    Task<IEnumerable<TModel>> ReadAll<TModel>(CancellationToken cancellationToken)
        where TModel : class;
}

/// <summary>
/// Without this class every model would need its own read method, such as ReadAllTeams and ReadAllPlayers. This class
/// reads any model with one method, <see cref="ReadAll{TModel}"/>. What differs per project, the database and the
/// database model of each model, is given by the class that derives from this one.
/// Example: reading all teams reads every TeamDb from the team table and returns them as teams.
/// The steps:
/// <list type="number">
/// <item>
/// <see cref="FindDatabaseModel"/> gives the database model of the model.
/// Example: a team gives a TeamDb.
/// </item>
/// <item>
/// All data is read from that table. Linked data is not read.
/// Example: all TeamDb, without the players of each team.
/// </item>
/// <item>
/// The mapper copies the database models into models. The mapper is found in the project of the class that derives
/// from this one.
/// Example: the mapper between a team and a TeamDb copies all TeamDb into teams.
/// </item>
/// </list>
/// Finding the database model is slow, so it is done on the first call and remembered after that in
/// <see cref="ReadAllAsyncMethods"/>.
/// </summary>
public abstract class BaseDatabaseReadService : IDatabaseReadService
{
    /// <summary>
    /// A list of ReadAllAsync methods, one per class that derives from this one and model.
    /// <see cref="ReadAll{TModel}"/> is called many times for the same model, and creating its
    /// <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> every time would be slow. So each one is created once and kept
    /// here. The class that derives from this one is part of the key, because each one gives its own database models.
    /// They are held as a Delegate, which is not type specific.
    /// Example: the <c>ReadAllAsync&lt;Team, TeamDb&gt;()</c> method.
    /// </summary>
    private static readonly ConcurrentDictionary<(Type Service, Type Model), Delegate> ReadAllAsyncMethods = new();

    /// <summary>
    /// Read as ReadAll + MethodInfo: the information (MethodInfo) about the method
    /// <see cref="ReadAllAsync{TModel, TDatabaseModel}"/>. This field reads nothing itself.
    /// <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> needs a model and a database model, but the database model is
    /// only found while the program runs, so the code cannot name it.
    /// <see cref="CreateReadAllAsyncMethod{TModel}"/> uses it to set <see cref="ReadAllAsync{TModel, TDatabaseModel}"/>
    /// to a model and its database model.
    /// Example: ReadAllAsync set to Team and TeamDb.
    /// </summary>
    private static readonly MethodInfo ReadAllMethodInfo = typeof(BaseDatabaseReadService)
        .GetMethod(nameof(ReadAllAsync), BindingFlags.Instance | BindingFlags.Public)!;

    /// <inheritdoc/>
    public Task<IEnumerable<TModel>> ReadAll<TModel>(CancellationToken cancellationToken)
        where TModel : class
    {
        // ReadAll works for any model, but the ReadAllAsync that runs is made for one model and its database model. So
        // the ReadAllAsync for TModel is taken from the field ReadAllAsyncMethods, or created with the method
        // CreateReadAllAsyncMethod when the field has none yet. The field gives back a Delegate, which is not type
        // specific, so it is cast to the Func for TModel. ReadAllAsync is a method of this service, so the Func takes the
        // service as its first input, and that is why this is passed first.
        // Example: for Team, the Delegate is cast to the Func that reads all teams, and then it runs with this.
        var readAll = (Func<BaseDatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>>)ReadAllAsyncMethods
            .GetOrAdd((GetType(), typeof(TModel)), _ => CreateReadAllAsyncMethod<TModel>());

        return readAll(this, cancellationToken);
    }

    /// <summary>
    /// Does the actual reading. It needs the model and its database model, so it only runs through the Func that
    /// <see cref="CreateReadAllAsyncMethod{TModel}"/> makes. It reads all data from the table and copies it into models
    /// with its mapper. It is public only so <see cref="ReadAllMethodInfo"/> can find it without reading private
    /// methods.
    /// Example: set to Team and TeamDb, it reads all TeamDb and returns them as teams.
    /// </summary>
    public async Task<IEnumerable<TModel>> ReadAllAsync<TModel, TDatabaseModel>(CancellationToken cancellationToken)
        where TModel : class
        where TDatabaseModel : class
    {
        // Step 1: read all data from the table.
        // Example: all TeamDb.
        var list = await GetAllAsync<TDatabaseModel>(cancellationToken);

        // Step 2: find the mapper that copies the table data into models. The mappers are in the project of the class
        // that derives from this one, so the lookup searches the assembly of that class.
        // Example: the mapper between a team and a TeamDb.
        var mapper = MapperLookup.Find<TModel, TDatabaseModel>(GetType().Assembly);

        // Step 3: the mapper copies the database models into models.
        // Example: all TeamDb become teams.
        return mapper.Map(list);
    }

    /// <summary>Makes a new context for one read. The caller disposes it.</summary>
    protected abstract Task<DbContext> CreateContextAsync(CancellationToken cancellationToken);

    /// <summary>
    /// The database model of a model. Throws when there is none.
    /// Example: a team gives a TeamDb.
    /// </summary>
    protected abstract Type FindDatabaseModel(Type modelType);

    /// <summary>
    /// <see cref="ReadAll{TModel}"/> only knows the model, but <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> also
    /// needs its database model, which is only found while the program runs. So this method finds the database model
    /// with <see cref="FindDatabaseModel"/>. Then it takes the field <see cref="ReadAllMethodInfo"/>, which is the same
    /// for every model, and makes from it a <see cref="ReadAllAsync{TModel, TDatabaseModel}"/> set to the model and its
    /// database model. It returns that as a Func for TModel.
    /// Example: for Team it finds TeamDb and returns the Func that reads all teams.
    /// </summary>
    private Func<BaseDatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>> CreateReadAllAsyncMethod<TModel>()
        where TModel : class =>
        ReadAllMethodInfo
            .MakeGenericMethod(typeof(TModel), FindDatabaseModel(typeof(TModel)))
            .CreateDelegate<Func<BaseDatabaseReadService, CancellationToken, Task<IEnumerable<TModel>>>>();

    /// <summary>
    /// Reads all data from one table. Linked data is not read; those links stay empty.
    /// Example: all TeamDb from the team table, without the players of each team.
    /// </summary>
    private async Task<List<TDatabaseModel>> GetAllAsync<TDatabaseModel>(CancellationToken cancellationToken)
        where TDatabaseModel : class
    {
        await using var context = await CreateContextAsync(cancellationToken);

        // EF normally tracks every model it reads, so it can save changes later. This data is only read, never saved
        // back, so AsNoTracking switches the tracking off. That makes reading faster.
        return await context.Set<TDatabaseModel>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
