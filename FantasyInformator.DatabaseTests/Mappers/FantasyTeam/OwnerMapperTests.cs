using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.DatabaseTests.Mappers.FantasyTeam;

[TestClass]
public class OwnerMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var owner = SourceFactory.Create<DatabaseFantasyTeam.Owner>();

        var result = OwnerMapper.Map(owner);

        Assert.IsNotNull(result);
        MappedAssert.Equal(owner, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = OwnerMapper.Map(null);

        Assert.IsNull(result);
    }
}
