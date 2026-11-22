using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Models.Nfl;

public class NflTeamDb : BaseEntityDb
{
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    public List<PlayerDb>? Players { get; init; }

    public List<GameDb>? HomeGames { get; init; }

    public List<GameDb>? AwayGames { get; init; }
}
