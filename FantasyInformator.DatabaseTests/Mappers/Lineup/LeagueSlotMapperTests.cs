using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.DatabaseTests.Mappers.Lineup;

[TestClass]
public class LeagueSlotMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var leagueSlot = SourceFactory.Create<DatabaseLineup.LeagueSlot>();

        var result = LeagueSlotMapper.Map(leagueSlot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueSlot, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = LeagueSlotMapper.Map((DatabaseLineup.LeagueSlot?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabaseLineup.LeagueSlot>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
