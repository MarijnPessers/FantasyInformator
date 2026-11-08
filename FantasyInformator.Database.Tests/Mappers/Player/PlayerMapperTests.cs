using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Tests.Mappers.Player;

[TestClass]
public class PlayerMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var player = SourceFactory.Create<DatabasePlayer.Player>();

        var result = PlayerMapper.Map(player);

        Assert.IsNotNull(result);
        MappedAssert.Equal(player, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabasePlayer.Player>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
