namespace FantasyInformator.Models;

public class Season : TrackedModel
{
    /// <summary>The year the season starts in.</summary>
    public int Year { get; set; }

    public List<LeagueInstance> LeagueInstances { get; set; } = [];

    public List<Game> Games { get; set; } = [];
}
