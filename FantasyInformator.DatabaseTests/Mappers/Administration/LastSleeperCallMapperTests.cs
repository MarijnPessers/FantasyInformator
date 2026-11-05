using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class LastSleeperCallMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var lastSleeperCall = SourceFactory.Create<DatabaseModels.LastSleeperCall>();

        var result = LastSleeperCallMapper.ToModel(lastSleeperCall);

        Assert.IsNotNull(result);
        MappedAssert.Equal(lastSleeperCall, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = LastSleeperCallMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
