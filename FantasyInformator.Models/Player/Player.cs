namespace FantasyInformator.Models;

public class Player : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    /// <summary>Null when the player has no NFL team.</summary>
    public Guid? CurrentNflTeamId { get; set; }

    public NflTeam? CurrentNflTeam { get; set; }

    public List<PlayerPosition> PlayerPositions { get; set; } = [];

    public List<GameAppearance> GameAppearances { get; set; } = [];

    public List<RosteredPlayer> RosteredPlayers { get; set; } = [];

    public List<PlayerSeasonStats> PlayerSeasonStats { get; set; } = [];
}
