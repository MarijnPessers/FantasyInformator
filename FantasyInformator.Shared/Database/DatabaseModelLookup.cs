namespace FantasyInformator.Shared.Database;

/// <summary>
/// Reading and saving only get the model, but the database works with its database model. Every database model is
/// named after its model with a suffix added, and derives from one base class. So this class finds it by that name.
/// Searching through the types of a project is slow, so the database models are picked out once, when this class is
/// made.
/// Example: with the suffix Db, a team gives a TeamDb.
/// </summary>
/// <param name="databaseModelBaseType">The class every database model derives from. Its project is searched.</param>
/// <param name="databaseModelSuffix">What is added to the name of a model to get the name of its database model.</param>
public sealed class DatabaseModelLookup(Type databaseModelBaseType, string databaseModelSuffix)
{
    /// <summary>Every database model in the project of the base class.</summary>
    private readonly Type[] _databaseModels =
    [
        .. databaseModelBaseType.Assembly.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false } && x.IsAssignableTo(databaseModelBaseType)),
    ];

    /// <summary>
    /// The database model of <paramref name="modelType"/>. Throws when there is none or more than one.
    /// Example: with the suffix Db, a team gives a TeamDb.
    /// </summary>
    public Type Find(Type modelType) =>
        _databaseModels.Single(x => x.Name == modelType.Name + databaseModelSuffix);
}
