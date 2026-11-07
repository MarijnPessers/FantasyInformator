using FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Services.Interfaces;

/// <summary>
/// Retrieves the data of the League group: <c>League</c> and <c>LeagueInstance</c>.
/// </summary>
public interface ILeagueService
{
    /// <summary>
    /// Gets the leagues a user plays in during one season.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/user/&lt;user_id&gt;/leagues/nfl/&lt;season&gt;</c></para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>league_id</c>: <c>LeagueInstance.ExternalId</c></item>
    /// <item><c>name</c>: <c>League.Name</c></item>
    /// <item><c>season</c>: the <c>Season</c> of <c>LeagueInstance</c></item>
    /// <item><c>previous_league_id</c>: links this <c>LeagueInstance</c> to the <c>League</c>
    /// of the previous season</item>
    /// <item><c>roster_positions</c>, <c>settings.taxi_slots</c>, <c>settings.reserve_slots</c>:
    /// <c>LeagueSlot</c> (Lineup group) and its <c>Count</c></item>
    /// </list>
    /// <para>Not used: <c>total_rosters</c>, <c>status</c>, <c>sport</c>, <c>season_type</c>, <c>draft_id</c>,
    /// <c>avatar</c>, <c>scoring_settings</c>, and every other key of <c>settings</c>.</para>
    /// </remarks>
    Task<IReadOnlyList<League>> GetLeaguesOfUserAsync(string userId, int season, CancellationToken cancellationToken);

    /// <inheritdoc cref="GetLeaguesOfUserAsync(string, int, CancellationToken)"/>
    Task<IReadOnlyList<League>> GetLeaguesOfUserAsync(string userId, int season) =>
        GetLeaguesOfUserAsync(userId, season, CancellationToken.None);

    /// <summary>
    /// Gets one league, to refresh a league that is already known.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/league/&lt;league_id&gt;</c></para>
    /// <para>Used and not used: the same as <see cref="GetLeaguesOfUserAsync(string, int, CancellationToken)"/>,
    /// which returns the same object.</para>
    /// </remarks>
    Task<League> GetLeagueAsync(string leagueId, CancellationToken cancellationToken);

    /// <inheritdoc cref="GetLeagueAsync(string, CancellationToken)"/>
    Task<League> GetLeagueAsync(string leagueId) =>
        GetLeagueAsync(leagueId, CancellationToken.None);
}
