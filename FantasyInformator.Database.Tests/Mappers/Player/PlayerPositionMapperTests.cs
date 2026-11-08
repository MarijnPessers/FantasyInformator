using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Tests.Mappers.Player;

[TestClass]
public class PlayerPositionMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var playerPosition = SourceFactory.Create<DatabasePlayer.PlayerPosition>();

        var result = PlayerPositionMapper.Map(playerPosition);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerPosition, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabasePlayer.PlayerPosition>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
