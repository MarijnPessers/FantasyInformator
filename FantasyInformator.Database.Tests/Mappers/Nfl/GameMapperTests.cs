using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class GameMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var game = SourceFactory.Create<DatabaseNfl.Game>();

        var result = GameMapper.Map(game);

        Assert.IsNotNull(result);
        MappedAssert.Equal(game, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<DatabaseNfl.Game>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
