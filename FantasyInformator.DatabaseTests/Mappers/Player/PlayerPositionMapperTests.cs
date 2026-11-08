using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class PlayerPositionMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var playerPosition = SourceFactory.Create<DatabasePlayer.PlayerPosition>();

        var result = PlayerPositionMapper.Map(playerPosition);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerPosition, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = PlayerPositionMapper.Map(null);

        Assert.IsNull(result);
    }
}
