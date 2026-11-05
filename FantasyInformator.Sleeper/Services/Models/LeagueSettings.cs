namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// The <c>settings</c> object inside a league. The Sleeper docs do not list its keys,
/// so only the two keys the application uses are held.
/// </summary>
public class LeagueSettings
{
    /// <summary>The number of taxi slots. Not part of <see cref="League.RosterPositions"/>.</summary>
    public int TaxiSlots { get; set; }

    /// <summary>The number of injured reserve slots. Not part of <see cref="League.RosterPositions"/>.</summary>
    public int ReserveSlots { get; set; }
}
