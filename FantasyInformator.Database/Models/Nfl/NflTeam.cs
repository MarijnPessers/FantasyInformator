
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Nfl;

public class NflTeam : TrackedModel
{
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    public List<Player.Player> Players { get; init; } = [];

    public List<Game> HomeGames { get; init; } = [];

    public List<Game> AwayGames { get; init; } = [];
}
