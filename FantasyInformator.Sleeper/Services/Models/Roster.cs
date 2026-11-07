namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// One roster of <c>GET /league/&lt;league_id&gt;/rosters</c>. Holds only the fields the application uses.
/// All lists hold Sleeper player ids, which can also be a team abbreviation such as "DET" for a defense.
/// </summary>
public class Roster
{
    /// <summary>The number of the roster within its league. It is not unique across leagues.</summary>
    public int RosterId { get; set; }

    /// <summary>The user id of the owner. Null when the roster has no owner.</summary>
    public string? OwnerId { get; set; }

    /// <summary>Every player on the roster, starters, IR and taxi included.</summary>
    public List<string> Players { get; init; } = [];

    public List<string> Starters { get; init; } = [];

    /// <summary>The players on injured reserve.</summary>
    public List<string> Reserve { get; init; } = [];

    /// <summary>The players on the taxi squad.</summary>
    public List<string> Taxi { get; init; } = [];
}
