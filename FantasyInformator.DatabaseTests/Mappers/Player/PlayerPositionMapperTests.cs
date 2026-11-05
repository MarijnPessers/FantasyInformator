using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class PlayerPositionMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var playerPosition = SourceFactory.Create<DatabaseModels.PlayerPosition>();

        var result = PlayerPositionMapper.ToModel(playerPosition);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerPosition, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = PlayerPositionMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
