
using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.FantasyTeams;

public class FantasyTeamDb : TrackedModelDb
{
    public required string ExternalId { get; set; }

    /// <summary>Null when the team has no owner.</summary>
    public Guid? OwnerId { get; set; }

    public OwnerDb? Owner { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstanceDb? LeagueInstance { get; set; }

    public required string Name { get; set; }

    public List<RosteredPlayerDb>? RosteredPlayers { get; init; }
}
