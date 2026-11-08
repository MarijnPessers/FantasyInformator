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
        var result = PlayerPositionMapper.Map((DatabasePlayer.PlayerPosition?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabasePlayer.PlayerPosition>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
