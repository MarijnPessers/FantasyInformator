namespace FantasyInformator.Database.Models;

public class Owner : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<FantasyTeam> FantasyTeams { get; set; } = [];
}
