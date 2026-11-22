using FantasyInformator.Database.Models.Lineups;

namespace FantasyInformator.Database.Models.Players;

/// <summary>
/// A position a player plays, such as QB or LB. Added when a player arrives with an unknown position.
/// </summary>
public class PositionDb : BaseEntityDb
{
    public required string Name { get; set; }

    public required string Abbreviation { get; set; }

    public bool IsDominant { get; set; }

    public List<PlayerPositionDb>? PlayerPositions { get; init; }

    public List<SlotEligibilityDb>? SlotEligibilities { get; init; }
}
