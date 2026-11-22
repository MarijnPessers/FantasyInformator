using FantasyInformator.Database.Mappers.Players;
using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Tests.Mappers.Players;

[TestClass]
public class PlayerMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var player = SourceFactory.Create<PlayerDb>();

        var result = PlayerMapper.Map(player);

        Assert.IsNotNull(result);
        MappedAssert.Equal(player, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<PlayerDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
