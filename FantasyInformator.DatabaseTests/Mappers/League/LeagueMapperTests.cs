using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class LeagueMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var league = SourceFactory.Create<DatabaseModels.League>();

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
        var league = new DatabaseModels.League();
        var leagueInstance = new DatabaseModels.LeagueInstance { League = league };
        league.LeagueInstances.Add(leagueInstance);

        var result = LeagueMapper.ToModel(league);

        Assert.IsNotNull(result);
        Assert.HasCount(1, result.LeagueInstances);
        Assert.AreSame(result, result.LeagueInstances[0].League);
    }
}
