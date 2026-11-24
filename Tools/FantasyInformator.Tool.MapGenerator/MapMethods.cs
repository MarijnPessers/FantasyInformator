using System.Reflection;

namespace FantasyInformator.Tool.MapGenerator;

public static class MapMethods
{
    /// <summary>
    /// Finds one Map method per class: first both ways of every explicit pair, then both ways of every model in
    /// <paramref name="modelsAssembly"/> that derives from the model base, for each class that has none yet.
    /// The other model of a model is its name with the prefix and suffix, in the other models namespace.
    /// Example: FantasyInformator.Domain.Players.Player with suffix Db and other models namespace
    /// FantasyInformator.Database.Models gives FantasyInformator.Database.Models.Players.PlayerDb.
    /// Throws a <see cref="MapGeneratorException"/> when a class is in two explicit pairs, the model base is missing,
    /// or two classes with a Map method have the same name.
    /// </summary>
    public static IReadOnlyList<MapMethod> Find(Assembly modelsAssembly, MapGeneratorArguments arguments)
    {
        List<MapMethod> mapMethods = [];

        foreach (var (model, otherModel) in arguments.ExplicitPairs)
        {
            foreach (var mapMethod in CreateBothWays(model, otherModel))
            {
                if (HasMapMethod(mapMethods, mapMethod.Source))
                {
                    throw new MapGeneratorException(
                        $"{mapMethod.Source} is in more than one {MapGeneratorArguments.PairName}.");
                }

                mapMethods.Add(mapMethod);
            }
        }

        var modelsNamespace = modelsAssembly.GetName().Name ?? string.Empty;

        foreach (var model in FindModels(modelsAssembly, arguments.ModelBaseClass))
        {
            var modelName = model.FullName!;
            var otherModel = CreateOtherModelName(model, modelsNamespace, arguments);

            foreach (var mapMethod in CreateBothWays(modelName, otherModel))
            {
                if (!HasMapMethod(mapMethods, mapMethod.Source))
                {
                    mapMethods.Add(mapMethod);
                }
            }
        }

        ThrowWhenSourceNamesRepeat(mapMethods);

        return mapMethods;
    }

    private static MapMethod[] CreateBothWays(string model, string otherModel) =>
        [new(model, otherModel), new(otherModel, model)];

    private static bool HasMapMethod(List<MapMethod> mapMethods, string source) =>
        mapMethods.Exists(x => x.Source == source);

    private static IEnumerable<Type> FindModels(Assembly modelsAssembly, string modelBaseClass)
    {
        var modelBase = modelsAssembly.GetType(modelBaseClass)
            ?? throw new MapGeneratorException($"{modelsAssembly.GetName().Name} has no class {modelBaseClass}.");

        return modelsAssembly.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false, IsGenericType: false, IsNested: false }
                && x.IsSubclassOf(modelBase))
            .OrderBy(x => x.FullName, StringComparer.Ordinal);
    }

    private static string CreateOtherModelName(Type model, string modelsNamespace, MapGeneratorArguments arguments)
    {
        var subNamespace = model.Namespace switch
        {
            var modelNamespace when modelNamespace == modelsNamespace => string.Empty,
            { } modelNamespace when modelNamespace.StartsWith($"{modelsNamespace}.", StringComparison.Ordinal) =>
                modelNamespace[modelsNamespace.Length..],
            _ => throw new MapGeneratorException(
                $"{model.FullName} is not in the namespace {modelsNamespace}, so its other model can't be named."),
        };

        return $"{arguments.OtherModelsNamespace}{subNamespace}.{arguments.Prefix}{model.Name}{arguments.Suffix}";
    }

    private static void ThrowWhenSourceNamesRepeat(List<MapMethod> mapMethods)
    {
        var repeatedSourceNames = mapMethods
            .GroupBy(x => x.SourceName)
            .Where(x => x.Count() > 1)
            .Select(x => $"{x.Key}: {string.Join(", ", x.Select(y => y.Source))}")
            .ToList();

        if (repeatedSourceNames.Count > 0)
        {
            throw new MapGeneratorException(
                "Classes with the same name would get the same MapExtensions class: "
                + $"{string.Join("; ", repeatedSourceNames)}.");
        }
    }
}
