using FantasyInformator.Database.Mappers.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.DatabaseTests.Mappers.League;

[TestClass]
public class LeagueMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var league = SourceFactory.Create<DatabaseLeague.League>();

        var result = LeagueMapper.ToModel(league);

        Assert.IsNotNull(result);
        MappedAssert.Equal(league, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = LeagueMapper.ToModel(null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void ToModel_LeagueAndInstanceLinkToEachOther_MappingEndsAndKeepsTheLoop()
    {
        var league = new DatabaseLeague.League();
        var leagueInstance = new DatabaseLeague.LeagueInstance { League = league };
        league.LeagueInstances.Add(leagueInstance);

        var result = LeagueMapper.ToModel(league);

        Assert.IsNotNull(result);
        Assert.HasCount(1, result.LeagueInstances);
        Assert.AreSame(result, result.LeagueInstances[0].League);
    }
}
