
using FantasyInformator.Domain.League;
using FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Domain.Nfl;

public class Season : TrackedModel
{
    /// <summary>The year the season starts in.</summary>
    public int Year { get; set; }

    /// <summary>
    /// The current week of the season. Only set on the current season, null on every other. Not stored.
    /// </summary>
    public int? CurrentWeek { get; set; }

    public List<LeagueInstance> LeagueInstances { get; init; } = [];

    public List<Game> Games { get; init; } = [];
}
