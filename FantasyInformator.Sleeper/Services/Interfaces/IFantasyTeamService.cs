using FantasyInformator.Models.FantasyTeam;
using FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Services.Interfaces;

/// <summary>
/// Retrieves the data of the FantasyTeam group: <c>Owner</c>, <c>FantasyTeam</c> and <c>RosteredPlayer</c>.
/// </summary>
public interface IFantasyTeamService
{
    /// <summary>
    /// Gets a user by username, to find the user id that the leagues call needs.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/user/&lt;username&gt;</c></para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>user_id</c>: <c>Owner.ExternalId</c></item>
    /// <item><c>display_name</c>: <c>Owner.Name</c></item>
    /// </list>
    /// <para>Not used: <c>username</c> (it can change, so the user id is the key), <c>avatar</c>.</para>
    /// </remarks>
    Task<Owner> GetUserAsync(string username, CancellationToken cancellationToken);

    /// <inheritdoc cref="GetUserAsync(string, CancellationToken)"/>
    Task<Owner> GetUserAsync(string username) =>
        GetUserAsync(username, CancellationToken.None);

    /// <summary>
    /// Gets the users of a league, the owners of its teams.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/league/&lt;league_id&gt;/users</c></para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>user_id</c>: <c>Owner.ExternalId</c>, and the match with <c>owner_id</c> of a roster</item>
    /// <item><c>display_name</c>: <c>Owner.Name</c></item>
    /// <item><c>metadata.team_name</c>: <c>FantasyTeam.Name</c></item>
    /// </list>
    /// <para>Not used: <c>username</c>, <c>avatar</c>, <c>is_owner</c> (commissioner),
    /// and any other key of <c>metadata</c>.</para>
    /// </remarks>
    Task<IReadOnlyList<LeagueUser>> GetLeagueUsersAsync(string leagueId, CancellationToken cancellationToken);

    /// <inheritdoc cref="GetLeagueUsersAsync(string, CancellationToken)"/>
    Task<IReadOnlyList<LeagueUser>> GetLeagueUsersAsync(string leagueId) =>
        GetLeagueUsersAsync(leagueId, CancellationToken.None);

    /// <summary>
    /// Gets the rosters of all teams in a league, in one call.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/league/&lt;league_id&gt;/rosters</c></para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>roster_id</c>: <c>FantasyTeam.ExternalId</c></item>
    /// <item><c>owner_id</c>: the owner of the <c>FantasyTeam</c>, kept as a plain value</item>
    /// <item><c>players</c>: <c>RosteredPlayer.PlayerId</c>, kept as a plain value</item>
    /// <item><c>starters</c>, <c>reserve</c>, <c>taxi</c>: <c>RosteredPlayer.SlotId</c>.
    /// A player in none of them is on the bench.
    /// A starter's slot follows from its place in the ordered <c>starters</c> list; that this order equals the order
    /// of <c>roster_positions</c> is not verified.</item>
    /// </list>
    /// <para>Not used: <c>league_id</c> (already known from the call), <c>settings</c> (wins, losses, ties, fpts,
    /// fpts_against, fpts_decimal, fpts_against_decimal, waiver_position, waiver_budget_used, total_moves).</para>
    /// </remarks>
    Task<IReadOnlyList<Roster>> GetRostersAsync(string leagueId, CancellationToken cancellationToken);

    /// <inheritdoc cref="GetRostersAsync(string, CancellationToken)"/>
    Task<IReadOnlyList<Roster>> GetRostersAsync(string leagueId) =>
        GetRostersAsync(leagueId, CancellationToken.None);
}
