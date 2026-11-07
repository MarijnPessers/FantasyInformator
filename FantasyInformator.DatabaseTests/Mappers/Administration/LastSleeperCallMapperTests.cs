using FantasyInformator.Database.Mappers.Administration;
using DatabaseAdministration = FantasyInformator.Database.Models.Administration;

namespace FantasyInformator.DatabaseTests.Mappers.Administration;

[TestClass]
public class LastSleeperCallMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var lastSleeperCall = SourceFactory.Create<DatabaseAdministration.LastSleeperCall>();

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
