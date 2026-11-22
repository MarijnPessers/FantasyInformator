using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Database.Models.Tracking;
using FantasyInformator.Domain.Enums;

namespace FantasyInformator.Database.Models.Players;

public class PlayerDb : TrackedModelDb
{
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    /// <summary>Null when the player has no NFL team.</summary>
    public Guid? CurrentNflTeamId { get; set; }

    public NflTeamDb? CurrentNflTeam { get; set; }

    public InjuryStatus InjuryStatus { get; set; }

    public List<PlayerPositionDb>? PlayerPositions { get; init; }

    public List<GameAppearanceDb>? GameAppearances { get; init; }

    public List<RosteredPlayerDb>? RosteredPlayers { get; init; }

    public List<PlayerSeasonStatsDb>? PlayerSeasonStats { get; init; }
}
