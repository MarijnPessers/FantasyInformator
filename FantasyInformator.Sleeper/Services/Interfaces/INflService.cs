using FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Services.Interfaces;

/// <summary>
/// Retrieves the data of the Nfl group: <c>Season</c>, <c>NflTeam</c> and <c>Game</c>.
/// </summary>
public interface INflService
{
    /// <summary>
    /// Gets the current season and week. Called before the first Sleeper call of a session.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/state/nfl</c></para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>season</c>: <c>Season.Year</c> of the current season. Kept in state, never saved.</item>
    /// <item><c>week</c>: the current week. Kept in state, never saved.</item>
    /// </list>
    /// <para>Not used: <c>season_type</c>, <c>season_start_date</c>, <c>previous_season</c>, <c>leg</c>,
    /// <c>league_season</c>, <c>league_create_season</c>, <c>display_week</c>.</para>
    /// </remarks>
    Task<State> GetStateAsync(CancellationToken cancellationToken);

    /// <inheritdoc cref="GetStateAsync(CancellationToken)"/>
    Task<State> GetStateAsync() =>
        GetStateAsync(CancellationToken.None);

    /// <summary>
    /// Gets the schedule of the regular season. Unofficial call, so it can change or be blocked.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.com/schedule/nfl/regular/&lt;season&gt;</c></para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>game_id</c>: <c>Game.ExternalId</c></item>
    /// <item><c>week</c>: <c>Game.Week</c></item>
    /// <item><c>home</c> and <c>away</c>: <c>Game.HomeNflTeamId</c> and <c>Game.AwayNflTeamId</c>,
    /// and the <c>NflTeam.ExternalId</c> of a team that is not known yet.
    /// Only the abbreviation is given, so <c>NflTeam.Name</c> is not filled by Sleeper.</item>
    /// <item><c>date</c>: the day of <c>Game.KickoffUtc</c>. The call gives only the day, no time.
    /// The time of day comes from the nflverse schedule (project <c>FantasyInformator.Nflverse</c>).</item>
    /// </list>
    /// <para>Not used: <c>status</c>.</para>
    /// </remarks>
    Task<IReadOnlyList<Game>> GetScheduleAsync(int season, CancellationToken cancellationToken);

    /// <inheritdoc cref="GetScheduleAsync(int, CancellationToken)"/>
    Task<IReadOnlyList<Game>> GetScheduleAsync(int season) =>
        GetScheduleAsync(season, CancellationToken.None);
}
