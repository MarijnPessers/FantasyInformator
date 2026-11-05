using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class GameAppearanceMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var gameAppearance = SourceFactory.Create<DatabaseModels.GameAppearance>();

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
