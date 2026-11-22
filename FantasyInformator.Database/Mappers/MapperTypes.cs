namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Finding a Map method means searching through the types of the Database project, and reading those types is slow.
/// So the mapper classes are picked out once and kept here. A mapper class is a static class whose name ends in Mapper.
/// Example: PlayerMapper, LeagueMapper and every other mapper class.
/// </summary>
internal static class MapperTypes
{
    private const string MapperSuffix = "Mapper";

    /// <summary>Every mapper class in the Database project.</summary>
    public static Type[] All { get; } =
    [
        .. typeof(MapperTypes).Assembly.GetTypes()
            .Where(x => x is { IsAbstract: true, IsSealed: true } && x.Name.EndsWith(MapperSuffix, StringComparison.Ordinal)),
    ];
}
