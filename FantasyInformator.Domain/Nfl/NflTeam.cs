using FantasyInformator.Domain.Players;

namespace FantasyInformator.Domain.Nfl;

public class NflTeam : BaseEntity
{
    public required string ExternalId { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Player>? Players { get; init; }

    public List<Game>? HomeGames { get; init; }

    public List<Game>? AwayGames { get; init; }
}
