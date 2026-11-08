
using FantasyInformator.Database.Models.League;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.FantasyTeam;

public class FantasyTeam : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    /// <summary>Null when the team has no owner.</summary>
    public Guid? OwnerId { get; set; }

    public Owner? Owner { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<RosteredPlayer> RosteredPlayers { get; init; } = [];
}
