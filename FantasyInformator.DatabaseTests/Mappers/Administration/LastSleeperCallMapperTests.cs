using FantasyInformator.Database.Mappers.Administration;
using DatabaseAdministration = FantasyInformator.Database.Models.Administration;

namespace FantasyInformator.DatabaseTests.Mappers.Administration;

[TestClass]
public class LastSleeperCallMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var lastSleeperCall = SourceFactory.Create<DatabaseAdministration.LastSleeperCall>();

        var result = LastSleeperCallMapper.Map(lastSleeperCall);

        Assert.IsNotNull(result);
        MappedAssert.Equal(lastSleeperCall, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = LastSleeperCallMapper.Map(null);

        Assert.IsNull(result);
    }
}
