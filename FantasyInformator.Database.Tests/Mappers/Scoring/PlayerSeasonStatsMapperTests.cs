using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Tests.Mappers.Scoring;

[TestClass]
public class PlayerSeasonStatsMapperTests
{
    private PlayerSeasonStatsMapper _mapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = new PlayerSeasonStatsMapper();
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var playerSeasonStats = SourceFactory.Create<PlayerSeasonStatsDb>();

        var result = _mapper.Map(playerSeasonStats);

        Assert.IsNotNull(result);
        MappedAssert.Equal(playerSeasonStats, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<PlayerSeasonStatsDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
