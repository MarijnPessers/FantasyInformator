using FantasyInformator.Database.Mappers.Players;
using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Tests.Mappers.Players;

[TestClass]
public class PlayerPositionMapperTests
{
    private PlayerPositionMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new PlayerPositionMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var playerPosition = SourceFactory.Create<PlayerPositionDb>();

        var result = _mapper.Map(playerPosition);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerPosition, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<PlayerPositionDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
