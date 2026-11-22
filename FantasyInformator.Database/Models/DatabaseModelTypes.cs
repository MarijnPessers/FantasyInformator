namespace FantasyInformator.Database.Models;

/// <summary>
/// Finding a database model by its name means searching through the types of the Database project, and reading those
/// types is slow. So the database models are picked out once and kept here.
/// Example: PlayerDb, LeagueDb and every other model that builds on <see cref="BaseEntityDb"/>.
/// </summary>
internal static class DatabaseModelTypes
{
    private const string DatabaseModelSuffix = "Db";

    /// <summary>Every database model type in the Database project.</summary>
    public static Type[] All { get; } =
    [
        .. typeof(BaseEntityDb).Assembly.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false } && x.IsAssignableTo(typeof(BaseEntityDb))),
    ];

    /// <summary>
    /// Reading and saving only get the model, but the database works with its database model. Every database model is
    /// named after its model with Db added, so it is found by that name in <see cref="All"/>. Fails when there is none
    /// or more than one.
    /// Example: Player gives PlayerDb.
    /// </summary>
    public static Type Find(Type modelType) =>
        All.Single(x => x.Name == modelType.Name + DatabaseModelSuffix);
}
