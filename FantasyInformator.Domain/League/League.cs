
using FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Domain.League;

public class League : TrackedModel
{
    public required string Name { get; set; }

    public List<LeagueInstance> LeagueInstances { get; init; } = [];
}
