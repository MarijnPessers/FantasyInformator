using AutoFixture;
using FantasyInformator.Nflverse.Mappers;
using NflverseModels = FantasyInformator.Nflverse.Services.Models;

namespace FantasyInformator.Nflverse.Tests.Mappers;

[TestClass]
public class NflScheduleMapperTests
{
    private const string NflverseRamsCode = "LA";
    private const string SleeperRamsCode = "LAR";

    private readonly Fixture _fixture = new();

    [TestMethod]
    public void ToGame_ShouldConvertEasternDaylightTimeToUtc_WhenGameIsInSummer()
    {
        var game = CreateGame(new DateOnly(2025, 9, 4), new TimeOnly(20, 20));

        var result = NflScheduleMapper.ToGame(game);

        Assert.AreEqual(new DateTime(2025, 9, 5, 0, 20, 0, DateTimeKind.Utc), result.KickoffUtc);
    }

    [TestMethod]
    public void ToGame_ShouldConvertEasternStandardTimeToUtc_WhenGameIsInWinter()
    {
        var game = CreateGame(new DateOnly(2025, 12, 7), new TimeOnly(13, 0));

        var result = NflScheduleMapper.ToGame(game);

        Assert.AreEqual(new DateTime(2025, 12, 7, 18, 0, 0, DateTimeKind.Utc), result.KickoffUtc);
    }

    [TestMethod]
    public void ToGame_ShouldThrow_WhenTimeIsSkippedByDaylightSavingTime()
    {
        var game = CreateGame(new DateOnly(2025, 3, 9), new TimeOnly(2, 30));

        _ = Assert.ThrowsExactly<ArgumentException>(() => NflScheduleMapper.ToGame(game));
    }

    [TestMethod]
    public void ToGame_ShouldMapWeekAndTeams_WhenScheduleGameIsGiven()
    {
        var game = CreateGame(new DateOnly(2025, 9, 7), new TimeOnly(13, 0));
        game.Week = _fixture.Create<int>();

        var result = NflScheduleMapper.ToGame(game);

        Assert.AreEqual(game.Week, result.Week);
        Assert.AreEqual(game.HomeTeam, result.HomeNflTeam!.ExternalId);
        Assert.AreEqual(game.AwayTeam, result.AwayNflTeam!.ExternalId);
    }

    [TestMethod]
    public void ToGame_ShouldUseSleeperCode_WhenTeamCodeDiffersFromSleeper()
    {
        var game = CreateGame(new DateOnly(2025, 9, 7), new TimeOnly(13, 0));
        game.HomeTeam = NflverseRamsCode;
        game.AwayTeam = NflverseRamsCode;

        var result = NflScheduleMapper.ToGame(game);

        Assert.AreEqual(SleeperRamsCode, result.HomeNflTeam!.ExternalId);
        Assert.AreEqual(SleeperRamsCode, result.AwayNflTeam!.ExternalId);
    }

    [TestMethod]
    public void ToGame_ShouldThrow_WhenHomeTeamIsEmpty()
    {
        var game = CreateGame(new DateOnly(2025, 9, 7), new TimeOnly(13, 0));
        game.HomeTeam = string.Empty;

        _ = Assert.ThrowsExactly<ArgumentException>(() => NflScheduleMapper.ToGame(game));
    }

    [TestMethod]
    public void ToGame_ShouldThrow_WhenAwayTeamIsEmpty()
    {
        var game = CreateGame(new DateOnly(2025, 9, 7), new TimeOnly(13, 0));
        game.AwayTeam = string.Empty;

        _ = Assert.ThrowsExactly<ArgumentException>(() => NflScheduleMapper.ToGame(game));
    }

    private NflverseModels.Game CreateGame(DateOnly gameday, TimeOnly gametime) =>
        new()
        {
            Gameday = gameday,
            Gametime = gametime,
            HomeTeam = _fixture.Create<string>(),
            AwayTeam = _fixture.Create<string>(),
        };
}
