using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Models.Lineups;

/// <summary>A slot a position can start in. A position has one or more.</summary>
public class SlotEligibilityDb : BaseEntityDb
{
    public Guid PositionId { get; set; }

    public PositionDb? Position { get; set; }

    public Guid SlotId { get; set; }

    public SlotDb? Slot { get; set; }

    /// <summary>True when the position is preferred to start in this slot.</summary>
    public bool IsPreferred { get; set; }
}
