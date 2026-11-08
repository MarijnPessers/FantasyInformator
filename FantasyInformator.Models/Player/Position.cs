
using FantasyInformator.Models.Lineup;
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.Player;

/// <summary>
/// A position a player plays, such as QB or LB. Added when a player arrives with an unknown position.
/// </summary>
public class Position : TrackedModel
{
    public string Name { get; set; } = string.Empty;

    public string Abbreviation { get; set; } = string.Empty;

    /// <summary>
    /// True when this position wins over the other positions of a player: a player with a dominant position counts
    /// only as his dominant positions, such as DL or DE.
    /// </summary>
    public bool IsDominant { get; set; }

    public List<PlayerPosition> PlayerPositions { get; init; } = [];

    public List<SlotEligibility> SlotEligibilities { get; init; } = [];
}
