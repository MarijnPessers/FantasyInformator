using FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Services.Interfaces;

/// <summary>
/// Retrieves the data of the Scoring group: <c>FantasyScore</c>.
/// <c>PlayerSeasonStats</c> is calculated by the application and has no call.
/// </summary>
public interface IScoringService
{
    /// <summary>
    /// Gets the points of all players of a league in one week, scored with the scoring of that league.
    /// </summary>
    /// <remarks>
    /// <para>Call: <c>GET https://api.sleeper.app/v1/league/&lt;league_id&gt;/matchups/&lt;week&gt;</c>. It returns one object per team.</para>
    /// <para>Used for:</para>
    /// <list type="bullet">
    /// <item><c>players_points</c>: <c>FantasyScore.Points</c>. Null means the player did not play, 0 means active without score.
    /// The player's <c>GameAppearance</c> for that week, from <see cref="IPlayerService.GetWeekStatsAsync"/>, tells which game the points belong to.
    /// The map is assumed to cover only players on a roster of that league in that week; that is not verified.</item>
    /// </list>
    /// <para>Not used: <c>points</c> (team total), <c>custom_points</c>, <c>starters</c>, <c>starters_points</c>, <c>players</c>,
    /// <c>roster_id</c>, <c>matchup_id</c>.</para>
    /// </remarks>
    Task<IReadOnlyList<Matchup>> GetMatchupsAsync(string leagueId, int week, CancellationToken cancellationToken = default);
}
