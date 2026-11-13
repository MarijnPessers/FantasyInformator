
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.League;

public class League : TrackedModel
{
    public required string Name { get; set; }

    public List<LeagueInstance> LeagueInstances { get; init; } = [];
}
