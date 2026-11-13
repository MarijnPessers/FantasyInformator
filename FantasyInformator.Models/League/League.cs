
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.League;

public class League : TrackedModel
{
    public required string Name { get; set; }

    public List<LeagueInstance> LeagueInstances { get; init; } = [];
}
