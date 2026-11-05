using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class PlayerSeasonStatsMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var playerSeasonStats = SourceFactory.Create<DatabaseModels.PlayerSeasonStats>();

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
