using AutoFixture;
using FantasyInformator.Sleeper.Mappers;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Tests.Mappers;

[TestClass]
public class NflMapperTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void ToSeason_MapsYearAndCurrentWeek()
    {
        var state = new SleeperModels.State { Season = "2025", Week = _fixture.Create<int>() };

        var result = NflMapper.ToSeason(state);

        Assert.AreEqual(2025, result.Year);
        Assert.AreEqual(state.Week, result.CurrentWeek);
    }

    [TestMethod]
    public void ToSeason_SeasonNotANumber_Throws()
    {
        var state = new SleeperModels.State { Season = _fixture.Create<string>() };

        _ = Assert.ThrowsExactly<FormatException>(() => NflMapper.ToSeason(state));
    }

    [TestMethod]
    public void ToGame_MapsExternalIdWeekTeamsAndKickoffAtMidnightUtc()
    {
        var game = new SleeperModels.Game
        {
            GameId = _fixture.Create<string>(),
            Week = _fixture.Create<int>(),
            Date = new DateOnly(2025, 9, 7),
            Home = _fixture.Create<string>(),
            Away = _fixture.Create<string>(),
        };

        var result = NflMapper.ToGame(game);

        Assert.AreEqual(game.GameId, result.ExternalId);
        Assert.AreEqual(game.Week, result.Week);
        Assert.AreEqual(new DateTime(2025, 9, 7, 0, 0, 0, DateTimeKind.Utc), result.KickoffUtc);
        Assert.AreEqual(game.Home, result.HomeNflTeam!.ExternalId);
        Assert.AreEqual(game.Away, result.AwayNflTeam!.ExternalId);
    }
}
