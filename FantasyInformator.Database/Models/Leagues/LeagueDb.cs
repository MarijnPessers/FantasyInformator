namespace FantasyInformator.Database.Models.Leagues;

public class LeagueDb : BaseEntityDb
{
    public required string Name { get; set; }

    public List<LeagueInstanceDb>? LeagueInstances { get; init; }
}
