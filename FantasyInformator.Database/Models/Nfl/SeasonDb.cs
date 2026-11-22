using FantasyInformator.Database.Models.Leagues;

namespace FantasyInformator.Database.Models.Nfl;

public class SeasonDb : BaseEntityDb
{
    /// <summary>The year the season starts in.</summary>
    public int Year { get; set; }

    public List<LeagueInstanceDb>? LeagueInstances { get; init; }

    public List<GameDb>? Games { get; init; }
}
