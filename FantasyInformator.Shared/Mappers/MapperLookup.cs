using System.Collections.Concurrent;
using System.Reflection;

namespace FantasyInformator.Shared.Mappers;

public static class MapperLookup
{
    private static readonly ConcurrentDictionary<(Assembly Assembly, Type Source, Type Target), IMapper> Mappers = new();

    /// <summary>
    /// Returns the mapper for the types provided,  regardless of order since a mapper holds mappings both ways.
    /// Uses caching.
    /// Throws when there is none or more than one.
    /// </summary>
    public static Mapper<TModel, TOtherModel> Find<TModel, TOtherModel>(Assembly assembly)
        where TModel : class
        where TOtherModel : class =>
        Find(assembly, typeof(TModel), typeof(TOtherModel)) switch
        {
            Mapper<TModel, TOtherModel> mapper => mapper,

            // The mapper was declared with TOtherModel first, so it is given with the order turned around.
            var mapper => ((Mapper<TOtherModel, TModel>)mapper).Reversed,
        };

    private static IMapper Find(Assembly assembly, Type sourceType, Type targetType) =>
        Mappers.GetOrAdd(
            (assembly, sourceType, targetType),
            key => (IMapper)Activator.CreateInstance(FindMapper(key.Assembly, key.Source, key.Target))!);

    private static Type FindMapper(Assembly assembly, Type sourceType, Type targetType) =>
        assembly.GetTypes()
            .Where(type => !type.IsAbstract && CopiesBetween(type, sourceType, targetType))
            .ToList() switch
        {
            [var mapperType] => mapperType,
            [] => throw new MappingException(
                $"{assembly.GetName().Name} has no mapper between {sourceType.Name} and {targetType.Name}.",
                sourceType,
                targetType),
            var mapperTypes => throw new MappingException(
                $"{assembly.GetName().Name} has more than one mapper between {sourceType.Name} and {targetType.Name}: "
                + $"{string.Join(", ", mapperTypes.Select(x => x.Name))}.",
                sourceType,
                targetType),
        };

    private static bool CopiesBetween(Type mapperType, Type sourceType, Type targetType) =>
        FindMapperBaseOrNull(mapperType)?.GetGenericArguments() is [var model, var otherModel]
        && ((sourceType, targetType) == (model, otherModel) || (sourceType, targetType) == (otherModel, model));

    private static Type? FindMapperBaseOrNull(Type type)
    {
        for (var baseType = type.BaseType; baseType is not null; baseType = baseType.BaseType)
        {
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(Mapper<,>))
            {
                return baseType;
            }
        }

        return null;
    }
}
