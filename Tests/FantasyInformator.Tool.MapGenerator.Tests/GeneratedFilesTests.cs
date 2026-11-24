namespace FantasyInformator.Tool.MapGenerator.Tests;

[TestClass]
public class GeneratedFilesTests
{
    private const string ExtensionsNamespace = "Shop.Database";

    private MapMethod _mapMethod = null!;
    private static readonly string[] Expected = ["PlayerMapExtensions.g.cs", "MapExtensions.g.cs"];

    [TestInitialize]
    public void Initialize() =>
        _mapMethod = new("Shop.Domain.Players.Player", "Shop.Database.Models.Players.PlayerDb");

    [TestMethod]
    public void Create_ShouldGiveOneFilePerMapMethodAndOneGenericFile_WhenMapMethodIsGiven()
    {
        var result = GeneratedFiles.Create([_mapMethod], ExtensionsNamespace);

        Assert.AreSequenceEqual(
            Expected, [.. result.Keys], SequenceOrder.InAnyOrder);
    }

    [TestMethod]
    public void Create_ShouldWriteMapMethodsForOneSourceAndList_WhenMapMethodIsGiven()
    {
        var result = GeneratedFiles.Create([_mapMethod], ExtensionsNamespace)["PlayerMapExtensions.g.cs"];

        Assert.Contains("namespace Shop.Database;", result);
        Assert.Contains("internal static class PlayerMapExtensions", result);
        Assert.Contains(
            "global::Shop.Database.Models.Players.PlayerDb Map(this global::Shop.Domain.Players.Player source)",
            result);
        Assert.Contains(
            "this global::System.Collections.Generic.IEnumerable<global::Shop.Domain.Players.Player> sources",
            result);
    }

    [TestMethod]
    public void Create_ShouldWriteGenericMapMethodWithOut_WhenMapMethodIsGiven()
    {
        var result = GeneratedFiles.Create([_mapMethod], ExtensionsNamespace)["MapExtensions.g.cs"];

        Assert.Contains("Map<TSource, TTarget>(this TSource source, out TTarget target)", result);
    }
}
