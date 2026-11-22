using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Tests.Mappers.Scoring;

[TestClass]
public class PlayerSeasonStatsMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var playerSeasonStats = SourceFactory.Create<PlayerSeasonStatsDb>();

        var result = PlayerSeasonStatsMapper.Map(playerSeasonStats);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerSeasonStats, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<PlayerSeasonStatsDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
