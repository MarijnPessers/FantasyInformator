using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class GameMapperTests
{
    private GameMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new GameMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var game = SourceFactory.Create<GameDb>();

        var result = _mapper.Map(game);

        Assert.IsNotNull(result);
        MappedAssert.Equal(game, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<GameDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
