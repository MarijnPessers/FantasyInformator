
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Leagues;

public class LeagueDb : TrackedModelDb
{
    public required string Name { get; set; }

    public List<LeagueInstanceDb>? LeagueInstances { get; init; }
}
