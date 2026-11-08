using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class GameAppearanceMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var gameAppearance = SourceFactory.Create<DatabasePlayer.GameAppearance>();

        var result = GameAppearanceMapper.Map(gameAppearance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(gameAppearance, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = GameAppearanceMapper.Map((DatabasePlayer.GameAppearance?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabasePlayer.GameAppearance>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
