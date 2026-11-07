using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class PlayerMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var player = SourceFactory.Create<DatabasePlayer.Player>();

        var result = PlayerMapper.ToModel(player);

        Assert.IsNotNull(result);
        MappedAssert.Equal(player, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = PlayerMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
