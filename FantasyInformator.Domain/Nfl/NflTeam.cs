
using FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Domain.Nfl;

public class NflTeam : TrackedModel
{
    public required string ExternalId { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Player.Player> Players { get; init; } = [];

    public List<Game> HomeGames { get; init; } = [];

    public List<Game> AwayGames { get; init; } = [];
}
