using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class PlayerMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var player = SourceFactory.Create<DatabasePlayer.Player>();

        var result = PlayerMapper.Map(player);

        Assert.IsNotNull(result);
        MappedAssert.Equal(player, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = PlayerMapper.Map(null);

        Assert.IsNull(result);
    }
}
