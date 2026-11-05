using FantasyInformator.Nflverse.Services.Models;

namespace FantasyInformator.Nflverse.Services.Interfaces;

/// <summary>
/// Retrieves the kickoff times that Sleeper does not give, for <c>Game.KickoffUtc</c> of the Nfl group.
/// </summary>
public interface INflScheduleService
{
    /// <summary>
    /// Gets the schedule of one season, played games and games still to play.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://raw.githubusercontent.com/nflverse/nfldata/master/data/games.csv</c>.
    /// One public CSV file holds every game since 1999, so the rows of the other seasons are dropped after reading.
    /// It is not an official NFL source, so it can change or disappear.</para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>season</c>: the filter for the requested season</item>
    /// <item><c>game_type</c>: the filter for the regular season ("REG"), the same as the Sleeper schedule</item>
    /// <item><c>week</c>, <c>away_team</c>, <c>home_team</c>: the match with the games of the Sleeper schedule. A team code that differs
    /// from Sleeper's (the Rams are "LA", Sleeper writes "LAR") needs a mapping.</item>
    /// <item><c>gameday</c> and <c>gametime</c>: <c>Game.KickoffUtc</c>. The time is Eastern, so it is converted to UTC.</item>
    /// </list>
    /// <para>Not used: <c>game_id</c>, <c>weekday</c>, the scores and results, <c>location</c>, <c>overtime</c>, the ids at other sites,
    /// rest days, betting lines, <c>roof</c>, <c>surface</c>, <c>temp</c>, <c>wind</c>, quarterbacks, coaches, referee and stadium.</para>
    /// </remarks>
    Task<IReadOnlyList<Game>> GetScheduleAsync(int season, CancellationToken cancellationToken = default);
}
