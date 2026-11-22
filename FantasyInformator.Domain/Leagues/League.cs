namespace FantasyInformator.Domain.Leagues;

public class League : BaseEntity
{
    public required string Name { get; set; }

    public List<LeagueInstance>? LeagueInstances { get; init; }
}
