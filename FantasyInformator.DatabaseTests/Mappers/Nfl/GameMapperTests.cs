using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.DatabaseTests.Mappers.Nfl;

[TestClass]
public class GameMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var game = SourceFactory.Create<DatabaseNfl.Game>();

        var result = GameMapper.ToModel(game);

        Assert.IsNotNull(result);
        MappedAssert.Equal(game, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = GameMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
