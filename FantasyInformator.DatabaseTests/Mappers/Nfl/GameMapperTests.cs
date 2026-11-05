using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class GameMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var game = SourceFactory.Create<DatabaseModels.Game>();

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
