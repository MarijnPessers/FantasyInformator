using AppModels = FantasyInformator.Models;
using NflverseModels = FantasyInformator.Nflverse.Services.Models;

namespace FantasyInformator.Nflverse.Mappers;

/// <summary>
/// Maps a nflverse schedule row to a <c>Game</c> of the Nfl group.
/// </summary>
public static class NflScheduleMapper
{
    private const string EasternTimeZoneId = "America/New_York";

    private static readonly TimeZoneInfo EasternTimeZone = TimeZoneInfo.FindSystemTimeZoneById(EasternTimeZoneId);

    /// <summary>
    /// The kickoff in the file is Eastern time, so it is converted to UTC. The teams are stubs that hold only the
    /// <c>ExternalId</c>, written as Sleeper writes the code. The caller matches the game to the Sleeper game by
    /// week and teams. Throws when the time does not exist in Eastern time (the hour skipped when the clocks go forward).
    /// </summary>
    public static AppModels.Game ToGame(NflverseModels.Game game) => new()
    {
        Week = game.Week,
        KickoffUtc = TimeZoneInfo.ConvertTimeToUtc(game.Gameday.ToDateTime(game.Gametime), EasternTimeZone),
        HomeNflTeam = new AppModels.NflTeam { ExternalId = TeamCodes.ToSleeperCode(game.HomeTeam) },
        AwayNflTeam = new AppModels.NflTeam { ExternalId = TeamCodes.ToSleeperCode(game.AwayTeam) },
    };
}
