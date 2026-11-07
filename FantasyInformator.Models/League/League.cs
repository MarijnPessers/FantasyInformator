
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.League;

public class League : TrackedModel
{
    public string Name { get; set; } = string.Empty;

    public List<LeagueInstance> LeagueInstances { get; init; } = [];
}
