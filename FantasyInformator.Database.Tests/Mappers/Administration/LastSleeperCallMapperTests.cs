using FantasyInformator.Database.Mappers.Administration;
using DatabaseAdministration = FantasyInformator.Database.Models.Administration;

namespace FantasyInformator.Database.Tests.Mappers.Administration;

[TestClass]
public class LastSleeperCallMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndLinks_WhenDatabaseModelIsGiven()
    {
        var lastSleeperCall = SourceFactory.Create<DatabaseAdministration.LastSleeperCall>();

        var result = LastSleeperCallMapper.Map(lastSleeperCall);

        Assert.IsNotNull(result);
        MappedAssert.Equal(lastSleeperCall, result);
    }

    [TestMethod]
    public void Map_ShouldReturnNull_WhenModelIsNull()
    {
        var result = LastSleeperCallMapper.Map(null);

        Assert.IsNull(result);
    }
}
