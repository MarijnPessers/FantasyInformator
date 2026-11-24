namespace FantasyInformator.Tool.MapGenerator.Tests;

[TestClass]
public class MapGeneratorArgumentsTests
{
    private List<string> _args = null!;

    [TestInitialize]
    public void Initialize() =>
        _args =
        [
            MapGeneratorArguments.ModelsName, "Shop.Domain.dll",
            MapGeneratorArguments.ModelBaseName, "Shop.Domain.BaseEntity",
            MapGeneratorArguments.SuffixName, "Db",
            MapGeneratorArguments.OtherModelsNamespaceName, "Shop.Database.Models",
            MapGeneratorArguments.ExtensionsNamespaceName, "Shop.Database",
            MapGeneratorArguments.OutputName, "Generated",
        ];

    [TestMethod]
    public void Parse_ShouldReadEveryValue_WhenRequiredArgumentsAreGiven()
    {
        var result = MapGeneratorArguments.Parse(_args);

        Assert.AreEqual("Shop.Domain.dll", result.ModelsAssemblyPath);
        Assert.AreEqual("Shop.Domain.BaseEntity", result.ModelBaseClass);
        Assert.AreEqual(string.Empty, result.Prefix);
        Assert.AreEqual("Db", result.Suffix);
        Assert.AreEqual("Shop.Database.Models", result.OtherModelsNamespace);
        Assert.AreEqual("Shop.Database", result.ExtensionsNamespace);
        Assert.AreEqual("Generated", result.OutputFolder);
        Assert.IsEmpty(result.ExplicitPairs);
    }

    [TestMethod]
    public void Parse_ShouldReadExplicitPair_WhenPairIsGiven()
    {
        _args.AddRange([MapGeneratorArguments.PairName, "Shop.Domain.Player=Shop.Database.SuperPlayer"]);

        var result = MapGeneratorArguments.Parse(_args);

        Assert.AreEqual("Shop.Database.SuperPlayer", result.ExplicitPairs["Shop.Domain.Player"]);
    }

    [TestMethod]
    public void Parse_ShouldThrow_WhenRequiredArgumentIsMissing() =>
        _ = Assert.ThrowsExactly<MapGeneratorException>(
            () => MapGeneratorArguments.Parse([MapGeneratorArguments.SuffixName, "Db"]));

    [TestMethod]
    public void Parse_ShouldThrow_WhenPairHasNoEqualsSign()
    {
        _args.AddRange([MapGeneratorArguments.PairName, "Shop.Domain.Player"]);

        _ = Assert.ThrowsExactly<MapGeneratorException>(() => MapGeneratorArguments.Parse(_args));
    }

    [TestMethod]
    public void Parse_ShouldThrow_WhenArgumentIsUnknown()
    {
        _args.AddRange(["--colour", "Blue"]);

        _ = Assert.ThrowsExactly<MapGeneratorException>(() => MapGeneratorArguments.Parse(_args));
    }
}
