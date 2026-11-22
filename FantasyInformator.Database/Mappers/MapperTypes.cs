namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Finding a mapper means searching through the types of the Database project, and reading those types is slow.
/// So the mapper classes are picked out once and kept here. A mapper class is a class that derives from
/// <see cref="Mapper{TModel, TDatabaseModel}"/>.
/// Example: PlayerMapper, LeagueMapper and every other mapper class.
/// </summary>
internal static class MapperTypes
{
    /// <summary>Every mapper class in the Database project.</summary>
    public static Type[] All { get; } =
    [
        .. typeof(MapperTypes).Assembly.GetTypes()
            .Where(x => x.BaseType is { IsGenericType: true } baseType
                && baseType.GetGenericTypeDefinition() == typeof(Mapper<,>)),
    ];
}
