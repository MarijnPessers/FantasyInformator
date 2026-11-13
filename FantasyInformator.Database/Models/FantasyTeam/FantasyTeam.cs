
using FantasyInformator.Database.Models.League;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.FantasyTeam;

public class FantasyTeam : TrackedModel
{
    public required string ExternalId { get; set; }

    /// <summary>Null when the team has no owner.</summary>
    public Guid? OwnerId { get; set; }

    public Owner? Owner { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

    public required string Name { get; set; }

    public List<RosteredPlayer> RosteredPlayers { get; init; } = [];
}
