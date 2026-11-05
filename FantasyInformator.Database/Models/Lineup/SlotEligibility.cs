namespace FantasyInformator.Database.Models;

/// <summary>A slot a position can start in. A position has one or more.</summary>
public class SlotEligibility : TrackedModel
{
    public Guid PositionId { get; set; }

    public Position? Position { get; set; }

    public Guid SlotId { get; set; }

    public Slot? Slot { get; set; }

    /// <summary>True when the position is preferred to start in this slot.</summary>
    public bool IsPreferred { get; set; }
}
