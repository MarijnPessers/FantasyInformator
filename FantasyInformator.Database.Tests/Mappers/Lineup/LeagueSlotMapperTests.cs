using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Tests.Mappers.Lineup;

[TestClass]
public class LeagueSlotMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var leagueSlot = SourceFactory.Create<DatabaseLineup.LeagueSlot>();

        var result = LeagueSlotMapper.Map(leagueSlot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueSlot, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<DatabaseLineup.LeagueSlot>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
