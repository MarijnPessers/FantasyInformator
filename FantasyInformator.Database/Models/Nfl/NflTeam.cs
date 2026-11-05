namespace FantasyInformator.Database.Models;

public class NflTeam : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<Player> Players { get; set; } = [];

    public List<Game> HomeGames { get; set; } = [];

    public List<Game> AwayGames { get; set; } = [];
}
