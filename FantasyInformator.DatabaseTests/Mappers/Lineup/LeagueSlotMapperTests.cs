using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.DatabaseTests.Mappers.Lineup;

[TestClass]
public class LeagueSlotMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var leagueSlot = SourceFactory.Create<DatabaseLineup.LeagueSlot>();

        var result = LeagueSlotMapper.ToModel(leagueSlot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueSlot, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = LeagueSlotMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
