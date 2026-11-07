namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// One team's side of a matchup in <c>GET /league/&lt;league_id&gt;/matchups/&lt;week&gt;</c>.
/// <c>players_points</c> is not in the Sleeper docs; it is known from third-party code
/// and not yet seen in a live response.
/// Holds only the fields the application uses.
/// </summary>
public class Matchup
{
    /// <summary>
    /// Player id to the points scored with the scoring of the league. Null when the player did not play.
    /// </summary>
    public Dictionary<string, double?> PlayersPoints { get; init; } = [];
}
