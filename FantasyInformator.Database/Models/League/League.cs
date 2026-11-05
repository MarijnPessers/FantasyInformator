namespace FantasyInformator.Database.Models;

public class League : TrackedModel
{
    public string Name { get; set; } = string.Empty;

    public List<LeagueInstance> LeagueInstances { get; set; } = [];
}
