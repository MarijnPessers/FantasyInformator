using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class SeasonMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var season = SourceFactory.Create<DatabaseModels.Season>();

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
        var season = new DatabaseModels.Season();
        var firstInstance = new DatabaseModels.LeagueInstance { Season = season };
        var secondInstance = new DatabaseModels.LeagueInstance { Season = season };
        season.LeagueInstances.AddRange([firstInstance, secondInstance]);

        var result = SeasonMapper.ToModel(season);

        Assert.IsNotNull(result);
        Assert.HasCount(2, result.LeagueInstances);
        Assert.AreSame(result, result.LeagueInstances[0].Season);
        Assert.AreSame(result, result.LeagueInstances[1].Season);
    }
}
