using FantasyInformator.Database.Mappers.Players;
using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Tests.Mappers.Players;

[TestClass]
public class PlayerMapperTests
{
    private PlayerMapper _mapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = new PlayerMapper();
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var player = SourceFactory.Create<PlayerDb>();

        var result = _mapper.Map(player);

        Assert.IsNotNull(result);
        MappedAssert.Equal(player, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<PlayerDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
