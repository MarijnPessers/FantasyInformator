using FantasyInformator.Domain.Enums;
using FantasyInformator.Domain.FantasyTeams;
using FantasyInformator.Domain.Nfl;
using FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Domain.Players;

public class Player : BaseEntity
{
    public required string ExternalId { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>Null when the player has no NFL team.</summary>
    public Guid? CurrentNflTeamId { get; set; }

    public NflTeam? CurrentNflTeam { get; set; }

    public InjuryStatus InjuryStatus { get; set; }

    /// <summary>
    /// True when the player may be moved to an IR slot: the injury status is IR, PUP or OUT.
    /// Not stored: worked out from the injury status.
    /// </summary>
    public bool IsIrEligible =>
        InjuryStatus is InjuryStatus.IR or InjuryStatus.PUP or InjuryStatus.OUT;

    public List<PlayerPosition>? PlayerPositions { get; init; }

    public List<GameAppearance>? GameAppearances { get; init; }

    public List<RosteredPlayer>? RosteredPlayers { get; init; }

    public List<PlayerSeasonStats>? PlayerSeasonStats { get; init; }
}
