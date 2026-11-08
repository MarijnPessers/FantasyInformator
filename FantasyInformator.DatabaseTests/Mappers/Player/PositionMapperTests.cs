using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class PositionMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var position = SourceFactory.Create<DatabasePlayer.Position>();

        var result = PositionMapper.Map(position);

        Assert.IsNotNull(result);
        MappedAssert.Equal(position, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = PositionMapper.Map((DatabasePlayer.Position?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabasePlayer.Position>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
