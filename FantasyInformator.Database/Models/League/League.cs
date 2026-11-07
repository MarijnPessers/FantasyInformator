
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.League;

public class League : TrackedModel
{
    public string Name { get; set; } = string.Empty;

    public List<LeagueInstance> LeagueInstances { get; init; } = [];
}
