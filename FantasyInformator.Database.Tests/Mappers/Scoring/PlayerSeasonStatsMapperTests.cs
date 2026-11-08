using FantasyInformator.Database.Mappers.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Tests.Mappers.Scoring;

[TestClass]
public class PlayerSeasonStatsMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var playerSeasonStats = SourceFactory.Create<DatabaseScoring.PlayerSeasonStats>();

        var result = PlayerSeasonStatsMapper.Map(playerSeasonStats);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerSeasonStats, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseScoring.PlayerSeasonStats>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
