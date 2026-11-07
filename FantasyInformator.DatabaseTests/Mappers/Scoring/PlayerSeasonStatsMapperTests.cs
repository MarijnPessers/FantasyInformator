using FantasyInformator.Database.Mappers.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.DatabaseTests.Mappers.Scoring;

[TestClass]
public class PlayerSeasonStatsMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var playerSeasonStats = SourceFactory.Create<DatabaseScoring.PlayerSeasonStats>();

        var result = PlayerSeasonStatsMapper.ToModel(playerSeasonStats);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerSeasonStats, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = PlayerSeasonStatsMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
