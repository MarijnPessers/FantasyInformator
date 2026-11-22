using FantasyInformator.Database.Mappers.Lineups;
using FantasyInformator.Database.Models.Lineups;

namespace FantasyInformator.Database.Tests.Mappers.Lineups;

[TestClass]
public class LeagueSlotMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var leagueSlot = SourceFactory.Create<LeagueSlotDb>();

        var result = LeagueSlotMapper.Map(leagueSlot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueSlot, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<LeagueSlotDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
