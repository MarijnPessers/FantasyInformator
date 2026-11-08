using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Tests.Mappers.Player;

[TestClass]
public class GameAppearanceMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var gameAppearance = SourceFactory.Create<DatabasePlayer.GameAppearance>();

        var result = GameAppearanceMapper.Map(gameAppearance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(gameAppearance, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabasePlayer.GameAppearance>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
