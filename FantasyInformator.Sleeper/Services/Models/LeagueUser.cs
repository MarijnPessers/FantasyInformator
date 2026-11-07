namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// One user of <c>GET /league/&lt;league_id&gt;/users</c>. Holds only the fields the application uses.
/// </summary>
public class LeagueUser
{
    public string UserId { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public LeagueUserMetadata Metadata { get; set; } = new();
}
