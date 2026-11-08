using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.DatabaseTests.Mappers.Nfl;

[TestClass]
public class GameMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var game = SourceFactory.Create<DatabaseNfl.Game>();

        var result = GameMapper.Map(game);

        Assert.IsNotNull(result);
        MappedAssert.Equal(game, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = GameMapper.Map(null);

        Assert.IsNull(result);
    }
}
