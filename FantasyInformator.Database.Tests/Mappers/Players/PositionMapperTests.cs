using FantasyInformator.Database.Mappers.Players;
using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Tests.Mappers.Players;

[TestClass]
public class PositionMapperTests
{
    private PositionMapper _mapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = new PositionMapper();
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var position = SourceFactory.Create<PositionDb>();

        var result = _mapper.Map(position);

        Assert.IsNotNull(result);
        MappedAssert.Equal(position, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<PositionDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
