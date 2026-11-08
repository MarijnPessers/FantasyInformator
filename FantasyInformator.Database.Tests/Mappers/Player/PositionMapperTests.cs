using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Tests.Mappers.Player;

[TestClass]
public class PositionMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var position = SourceFactory.Create<DatabasePlayer.Position>();

        var result = PositionMapper.Map(position);

        Assert.IsNotNull(result);
        MappedAssert.Equal(position, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabasePlayer.Position>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
