
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Nfl;

public class NflTeamDb : TrackedModelDb
{
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    public List<Players.PlayerDb>? Players { get; init; }

    public List<GameDb>? HomeGames { get; init; }

    public List<GameDb>? AwayGames { get; init; }
}
