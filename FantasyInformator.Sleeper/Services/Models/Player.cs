namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// One player of <c>GET /players/nfl</c>, which returns an object keyed by player id.
/// Holds only the fields the application uses.
/// </summary>
public class Player
{
    public string PlayerId { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    /// <summary>The main position, for example "QB". Null for a player without one.</summary>
    public string? Position { get; set; }

    /// <summary>Every position the player can fill in a fantasy lineup.</summary>
    public List<string> FantasyPositions { get; set; } = [];

    /// <summary>The NFL team abbreviation, for example "KC". Null without an NFL team.</summary>
    public string? Team { get; set; }
}
