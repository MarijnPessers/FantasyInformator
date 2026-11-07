using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class PlayerPositionMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var playerPosition = SourceFactory.Create<DatabasePlayer.PlayerPosition>();

        var result = PlayerPositionMapper.ToModel(playerPosition);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerPosition, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = PlayerPositionMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
