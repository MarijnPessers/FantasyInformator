using System.Reflection;
using FantasyInformator.Tool.MapGenerator.Tests.Models;

namespace FantasyInformator.Tool.MapGenerator.Tests;

[TestClass]
public class MapMethodsTests
{
    private const string PlayerDb = "Shop.Database.Models.Models.PlayerDb";
    private const string SuperPlayer = "Shop.Database.SuperPlayer";

    private static readonly string PlayerName = typeof(Player).FullName!;

    private Assembly _modelsAssembly = null!;

    [TestInitialize]
    public void Initialize() =>
        _modelsAssembly = typeof(Player).Assembly;

    [TestMethod]
    public void Find_ShouldGiveMapMethodBothWays_WhenModelDerivesFromModelBase()
    {
        var result = MapMethods.Find(_modelsAssembly, CreateArguments([]));

        Assert.AreSequenceEqual(
            [new MapMethod(PlayerName, PlayerDb), new MapMethod(PlayerDb, PlayerName)], [.. result], SequenceOrder.InAnyOrder);
    }

    [TestMethod]
    public void Find_ShouldUseExplicitPair_WhenPairIsGivenForModel()
    {
        var result = MapMethods.Find(_modelsAssembly, CreateArguments(new() { [PlayerName] = SuperPlayer }));

        Assert.AreSequenceEqual(
            [
                new MapMethod(PlayerName, SuperPlayer),
                new MapMethod(SuperPlayer, PlayerName),
                new MapMethod(PlayerDb, PlayerName),
            ], [.. result], SequenceOrder.InAnyOrder);
    }

    [TestMethod]
    public void Find_ShouldThrow_WhenClassIsInTwoExplicitPairs()
    {
        var arguments = CreateArguments(new() { [PlayerName] = SuperPlayer, ["Shop.Domain.Team"] = SuperPlayer });

        _ = Assert.ThrowsExactly<MapGeneratorException>(() => MapMethods.Find(_modelsAssembly, arguments));
    }

    [TestMethod]
    public void Find_ShouldThrow_WhenModelBaseIsMissing()
    {
        var arguments = CreateArguments([]) with { ModelBaseClass = "Shop.Domain.Missing" };

        _ = Assert.ThrowsExactly<MapGeneratorException>(() => MapMethods.Find(_modelsAssembly, arguments));
    }

    private static MapGeneratorArguments CreateArguments(Dictionary<string, string> explicitPairs) =>
        new()
        {
            ModelsAssemblyPath = "Shop.Domain.dll",
            ModelBaseClass = typeof(ModelBase).FullName!,
            Prefix = string.Empty,
            Suffix = "Db",
            OtherModelsNamespace = "Shop.Database.Models",
            ExtensionsNamespace = "Shop.Database",
            OutputFolder = "Generated",
            ExplicitPairs = explicitPairs,
        };
}
