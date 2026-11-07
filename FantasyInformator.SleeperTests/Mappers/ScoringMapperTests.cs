using AutoFixture;
using FantasyInformator.Sleeper.Mappers;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.SleeperTests.Mappers;

[TestClass]
public class ScoringMapperTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void ToFantasyScores_KeepsPointsNullAndZeroApart()
    {
        List<string> playerIds = [.. _fixture.CreateMany<string>(3)];
        var matchup = new SleeperModels.Matchup
        {
            PlayersPoints = new Dictionary<string, double?>
            {
                [playerIds[0]] = 12.5,
                [playerIds[1]] = null,
                [playerIds[2]] = 0,
            },
        };

        var result = ScoringMapper.ToFantasyScores(matchup);

        var pointsByPlayer = result.ToDictionary(
            f => f.GameAppearance!.Player!.ExternalId,
            f => f.Points);
        Assert.AreEqual(12.5m, pointsByPlayer[playerIds[0]]);
        Assert.IsNull(pointsByPlayer[playerIds[1]]);
        Assert.AreEqual(0m, pointsByPlayer[playerIds[2]]);
    }

    [TestMethod]
    public void ToFantasyScores_NoPlayers_ReturnsEmptyList()
    {
        var matchup = new SleeperModels.Matchup();

        var result = ScoringMapper.ToFantasyScores(matchup);

        Assert.IsEmpty(result);
    }
}
