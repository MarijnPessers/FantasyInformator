namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// The league object of <c>GET /league/&lt;league_id&gt;</c>. The user-leagues call returns the same object.
/// Holds only the fields the application uses.
/// </summary>
public class League
{
    public string LeagueId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    /// <summary>The season as Sleeper writes it, for example "2025".</summary>
    public string Season { get; set; } = string.Empty;

    /// <summary>The league id of the previous season. Null for the first season of a league.</summary>
    public string? PreviousLeagueId { get; set; }

    /// <summary>The slots of the lineup, one entry per slot, for example "QB", "FLEX", "BN".</summary>
    public List<string> RosterPositions { get; init; } = [];

    public LeagueSettings Settings { get; set; } = new();
}
