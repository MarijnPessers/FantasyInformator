using FantasyInformator.Database.Mappers.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.DatabaseTests.Mappers.Player;

[TestClass]
public class PositionMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var position = SourceFactory.Create<DatabasePlayer.Position>();

        var result = PositionMapper.ToModel(position);

        Assert.IsNotNull(result);
        MappedAssert.Equal(position, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = PositionMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
