using FantasyInformator.Database.Mappers.Nfl;
using DatabaseLeague = FantasyInformator.Database.Models.League;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.DatabaseTests.Mappers.Nfl;

[TestClass]
public class SeasonMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var season = SourceFactory.Create<DatabaseNfl.Season>();

        var result = SeasonMapper.ToModel(season);

        Assert.IsNotNull(result);
        MappedAssert.Equal(season, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = SeasonMapper.ToModel(null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void ToModel_TwoLeagueInstancesShareTheSeason_MapsTheSeasonOnce()
    {
        var season = new DatabaseNfl.Season();
        var firstInstance = new DatabaseLeague.LeagueInstance { Season = season };
        var secondInstance = new DatabaseLeague.LeagueInstance { Season = season };
        season.LeagueInstances.AddRange([firstInstance, secondInstance]);

        var result = SeasonMapper.ToModel(season);

        Assert.IsNotNull(result);
        Assert.HasCount(2, result.LeagueInstances);
        Assert.AreSame(result, result.LeagueInstances[0].Season);
        Assert.AreSame(result, result.LeagueInstances[1].Season);
    }
}
