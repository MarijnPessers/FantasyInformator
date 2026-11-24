using FantasyInformator.Database.Mappers.Players;
using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Tests.Mappers.Players;

[TestClass]
public class GameAppearanceMapperTests
{
    private GameAppearanceMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new GameAppearanceMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var gameAppearance = SourceFactory.Create<GameAppearanceDb>();

        var result = _mapper.Map(gameAppearance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(gameAppearance, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<GameAppearanceDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
