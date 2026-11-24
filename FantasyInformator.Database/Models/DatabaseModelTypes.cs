using FantasyInformator.Shared.Database;

namespace FantasyInformator.Database.Models;

/// <summary>
/// The database models of this project: every model that builds on <see cref="BaseEntityDb"/>, named after its model
/// with Db added. Searching for them is slow, so <see cref="Lookup"/> picks them out once.
/// Example: PlayerDb, LeagueDb and every other database model.
/// </summary>
internal static class DatabaseModelTypes
{
    private const string DatabaseModelSuffix = "Db";

    private static DatabaseModelLookup Lookup { get; } = new(typeof(BaseEntityDb), DatabaseModelSuffix);

    /// <summary>
    /// The database model of a model. Throws when there is none or more than one.
    /// Example: Player gives PlayerDb.
    /// </summary>
    public static Type Find(Type modelType) =>
        Lookup.Find(modelType);
}
