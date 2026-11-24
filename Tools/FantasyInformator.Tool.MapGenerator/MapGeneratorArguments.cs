namespace FantasyInformator.Tool.MapGenerator;

public sealed record MapGeneratorArguments
{
    public const string ModelsName = "--models";
    public const string ModelBaseName = "--model-base";
    public const string PrefixName = "--prefix";
    public const string SuffixName = "--suffix";
    public const string OtherModelsNamespaceName = "--other-models-namespace";
    public const string ExtensionsNamespaceName = "--extensions-namespace";
    public const string OutputName = "--output";
    public const string PairName = "--pair";

    private static readonly HashSet<string> SingleValueNames =
    [
        ModelsName,
        ModelBaseName,
        PrefixName,
        SuffixName,
        OtherModelsNamespaceName,
        ExtensionsNamespaceName,
        OutputName,
    ];

    public required string ModelsAssemblyPath { get; init; }

    public required string ModelBaseClass { get; init; }

    public required string Prefix { get; init; }

    public required string Suffix { get; init; }

    public required string OtherModelsNamespace { get; init; }

    public required string ExtensionsNamespace { get; init; }

    public required string OutputFolder { get; init; }

    public required IReadOnlyDictionary<string, string> ExplicitPairs { get; init; }

    /// <summary>
    /// Reads the arguments as pairs of a name and its value. Only --pair may be given more than once.
    /// Throws a <see cref="MapGeneratorException"/> when an argument is unknown, missing or written wrong.
    /// </summary>
    public static MapGeneratorArguments Parse(IEnumerable<string> args)
    {
        var values = new Dictionary<string, string>();
        var explicitPairs = new Dictionary<string, string>();
        var queue = new Queue<string>(args);

        while (queue.TryDequeue(out var name))
        {
            if (!queue.TryDequeue(out var value))
            {
                throw new MapGeneratorException($"{name} has no value.");
            }

            if (name == PairName)
            {
                AddExplicitPair(explicitPairs, value);
            }
            else if (!SingleValueNames.Contains(name))
            {
                throw new MapGeneratorException($"{name} is not a known argument.");
            }
            else if (!values.TryAdd(name, value))
            {
                throw new MapGeneratorException($"{name} is given more than once.");
            }
        }

        return new()
        {
            ModelsAssemblyPath = FindRequiredValue(values, ModelsName),
            ModelBaseClass = FindRequiredValue(values, ModelBaseName),
            Prefix = values.GetValueOrDefault(PrefixName, string.Empty),
            Suffix = values.GetValueOrDefault(SuffixName, string.Empty),
            OtherModelsNamespace = FindRequiredValue(values, OtherModelsNamespaceName),
            ExtensionsNamespace = FindRequiredValue(values, ExtensionsNamespaceName),
            OutputFolder = FindRequiredValue(values, OutputName),
            ExplicitPairs = explicitPairs,
        };
    }

    private static void AddExplicitPair(Dictionary<string, string> explicitPairs, string value)
    {
        if (value.Split('=') is not [{ Length: > 0 } model, { Length: > 0 } otherModel])
        {
            throw new MapGeneratorException(
                $"{PairName} {value} is not written as Model=OtherModel, with the full name of both classes.");
        }

        if (!explicitPairs.TryAdd(model, otherModel))
        {
            throw new MapGeneratorException($"{PairName} is given more than once for {model}.");
        }
    }

    private static string FindRequiredValue(Dictionary<string, string> values, string name) =>
        values.TryGetValue(name, out var value) && value.Length > 0
            ? value
            : throw new MapGeneratorException($"{name} is required.");
}
