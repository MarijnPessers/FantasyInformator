
using FantasyInformator.Database.Models.Lineup;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Player;

/// <summary>
/// A position a player plays, such as QB or LB. Added when a player arrives with an unknown position.
/// </summary>
public class Position : TrackedModel
{
    public required string Name { get; set; }

    public required string Abbreviation { get; set; }

    public bool IsDominant { get; set; }

    public List<PlayerPosition> PlayerPositions { get; init; } = [];

    public List<SlotEligibility> SlotEligibilities { get; init; } = [];
}
