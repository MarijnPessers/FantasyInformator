using FantasyInformator.Database.Mappers.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.DatabaseTests.Mappers.League;

[TestClass]
public class LeagueMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var league = SourceFactory.Create<DatabaseLeague.League>();

        var result = LeagueMapper.Map(league);

        Assert.IsNotNull(result);
        MappedAssert.Equal(league, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = LeagueMapper.Map(null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_LeagueAndInstanceLinkToEachOther_MappingEndsAndKeepsTheLoop()
    {
        var league = new DatabaseLeague.League();
        var leagueInstance = new DatabaseLeague.LeagueInstance { League = league };
        league.LeagueInstances.Add(leagueInstance);

        var result = LeagueMapper.Map(league);

        Assert.IsNotNull(result);
        Assert.HasCount(1, result.LeagueInstances);
        Assert.AreSame(result, result.LeagueInstances[0].League);
    }
}
