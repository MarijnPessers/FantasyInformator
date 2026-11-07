using System.Globalization;
using FantasyInformator.Models.Nfl;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Mappers;

/// <summary>
/// Maps the Sleeper state and schedule to the Nfl group.
/// Links to other records are stubs that hold only the <c>ExternalId</c>.
/// </summary>
public static class NflMapper
{
    /// <summary>Throws when the season of the state is not a number.</summary>
    public static Season ToSeason(SleeperModels.State state) =>
        new()
        {
            Year = int.Parse(state.Season, CultureInfo.InvariantCulture),
        };

    /// <summary>
    /// The schedule gives only the day, so <c>KickoffUtc</c> is midnight UTC of that day until the time of day
    /// from the nflverse schedule replaces it. The season is not on the schedule game; the caller sets it.
    /// </summary>
    public static Game ToGame(SleeperModels.Game game) =>
        new()
        {
            ExternalId = game.GameId,
            Week = game.Week,
            KickoffUtc = game.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
            HomeNflTeam = new NflTeam { ExternalId = game.Home },
            AwayNflTeam = new NflTeam { ExternalId = game.Away },
        };
}
