
using FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Domain.Leagues;

public class League : TrackedModel
{
    public required string Name { get; set; }

    public List<LeagueInstance>? LeagueInstances { get; init; }
}
