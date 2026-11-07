
using FantasyInformator.Models.League;
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.Nfl;

public class Season : TrackedModel
{
    /// <summary>The year the season starts in.</summary>
    public int Year { get; set; }

    public List<LeagueInstance> LeagueInstances { get; init; } = [];

    public List<Game> Games { get; init; } = [];
}
