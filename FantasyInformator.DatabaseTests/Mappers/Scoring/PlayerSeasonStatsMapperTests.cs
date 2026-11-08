using FantasyInformator.Database.Mappers.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.DatabaseTests.Mappers.Scoring;

[TestClass]
public class PlayerSeasonStatsMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var playerSeasonStats = SourceFactory.Create<DatabaseScoring.PlayerSeasonStats>();

        var result = PlayerSeasonStatsMapper.Map(playerSeasonStats);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerSeasonStats, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = PlayerSeasonStatsMapper.Map((DatabaseScoring.PlayerSeasonStats?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabaseScoring.PlayerSeasonStats>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
