namespace FantasyInformator.Database.Models.Players;

/// <summary>A position a player plays. A player has one or more.</summary>
public class PlayerPositionDb : BaseEntityDb
{
    public Guid PlayerId { get; set; }

    public PlayerDb? Player { get; set; }

    public Guid PositionId { get; set; }

    public PositionDb? Position { get; set; }
}
