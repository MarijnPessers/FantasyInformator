
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.Nfl;

public class NflTeam : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<Player.Player> Players { get; init; } = [];

    public List<Game> HomeGames { get; init; } = [];

    public List<Game> AwayGames { get; init; } = [];
}
