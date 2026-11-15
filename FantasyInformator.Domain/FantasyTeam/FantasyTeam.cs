
using FantasyInformator.Domain.League;
using FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Domain.FantasyTeam;

public class FantasyTeam : TrackedModel
{
    public required string ExternalId { get; set; }

    /// <summary>Null when the team has no owner.</summary>
    public Guid? OwnerId { get; set; }

    public Owner? Owner { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<RosteredPlayer> RosteredPlayers { get; init; } = [];
}
