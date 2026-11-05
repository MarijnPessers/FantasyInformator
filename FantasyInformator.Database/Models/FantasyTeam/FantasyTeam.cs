namespace FantasyInformator.Database.Models;

public class FantasyTeam : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public Guid OwnerId { get; set; }

    public Owner? Owner { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<RosteredPlayer> RosteredPlayers { get; set; } = [];
}
