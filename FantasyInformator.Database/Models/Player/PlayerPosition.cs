namespace FantasyInformator.Database.Models;

/// <summary>A position a player plays. A player has one or more.</summary>
public class PlayerPosition : TrackedModel
{
    public Guid PlayerId { get; set; }

    public Player? Player { get; set; }

    public Guid PositionId { get; set; }

    public Position? Position { get; set; }
}
