
using FantasyInformator.Database.Models.FantasyTeam;
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Player;

public class Player : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    /// <summary>Null when the player has no NFL team.</summary>
    public Guid? CurrentNflTeamId { get; set; }

    public NflTeam? CurrentNflTeam { get; set; }

    public List<PlayerPosition> PlayerPositions { get; init; } = [];

    public List<GameAppearance> GameAppearances { get; init; } = [];

    public List<RosteredPlayer> RosteredPlayers { get; init; } = [];

    public List<PlayerSeasonStats> PlayerSeasonStats { get; init; } = [];
}
