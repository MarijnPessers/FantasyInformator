using System.Reflection;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// <see cref="Services.DatabaseReadService"/> and <see cref="MapContext"/> only know the two
/// models they copy between, not the mapper class that does the copying. This class finds the Map method from what goes
/// in and what comes out, so they never have to name the mapper.
/// Example: a list of PlayerDb in and a list of Players out gives the Map method in PlayerMapper.
/// </summary>
internal static class MapperLookup
{
    private const string MapMethodName = "Map";

    /// <summary>
    /// A Map method can't be found by its name alone, because every mapper class has several Map methods. So this
    /// method looks through all classes in <see cref="MapperTypes"/> for the one Map method whose input is exactly
    /// <paramref name="parameterTypes"/> and whose output is <paramref name="returnType"/>. Only public methods count.
    /// It throws when it finds none or more than one, because then a mapper is missing or written twice.
    /// Example: a list of PlayerDb in and a list of Players out gives the Map method in PlayerMapper.
    /// </summary>
    /// <param name="parameterTypes">
    /// The types of the Map method's inputs, in order. Usually there is one: the data to copy. Some Map methods take a
    /// second input that remembers what has already been copied, so a link back finds the same object instead of a copy.
    /// Example: a list of PlayerDb, or one PlayerDb and the MapContext. The MapContext keeps track of everything copied
    /// so far in one whole mapping call, such as a team and its players, so that a player's link back to its team points
    /// to that same team instead of a new copy.
    /// </param>
    public static MethodInfo Find(Type[] parameterTypes, Type returnType) =>
        MapperTypes.All
            .SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Static))
            .Single(x => x.Name == MapMethodName
                && x.ReturnType == returnType
                && x.GetParameters().Select(parameter => parameter.ParameterType).SequenceEqual(parameterTypes));
}
