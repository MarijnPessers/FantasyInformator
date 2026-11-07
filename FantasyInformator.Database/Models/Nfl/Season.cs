
using FantasyInformator.Database.Models.League;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Nfl;

public class Season : TrackedModel
{
    /// <summary>The year the season starts in.</summary>
    public int Year { get; set; }

    public List<LeagueInstance> LeagueInstances { get; init; } = [];

    public List<Game> Games { get; init; } = [];
}
