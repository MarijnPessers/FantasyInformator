using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class RosteredPlayerMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var rosteredPlayer = SourceFactory.Create<DatabaseModels.RosteredPlayer>();

        var result = RosteredPlayerMapper.ToModel(rosteredPlayer);

        Assert.IsNotNull(result);
        MappedAssert.Equal(rosteredPlayer, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = RosteredPlayerMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
