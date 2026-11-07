using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class GameAppearanceMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var gameAppearance = SourceFactory.Create<DatabasePlayer.GameAppearance>();

        var result = GameAppearanceMapper.ToModel(gameAppearance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(gameAppearance, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = GameAppearanceMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
