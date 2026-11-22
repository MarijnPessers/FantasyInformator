using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class GameMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var game = SourceFactory.Create<GameDb>();

        var result = GameMapper.Map(game);

        Assert.IsNotNull(result);
        MappedAssert.Equal(game, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<GameDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
