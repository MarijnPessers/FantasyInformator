using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class PlayerMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var player = SourceFactory.Create<DatabaseModels.Player>();

        var result = PlayerMapper.ToModel(player);

        Assert.IsNotNull(result);
        MappedAssert.Equal(player, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = PlayerMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
