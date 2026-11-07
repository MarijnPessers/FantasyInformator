using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.DatabaseTests.Mappers.FantasyTeam;

[TestClass]
public class OwnerMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var owner = SourceFactory.Create<DatabaseFantasyTeam.Owner>();

        var result = OwnerMapper.ToModel(owner);

        Assert.IsNotNull(result);
        MappedAssert.Equal(owner, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = OwnerMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
