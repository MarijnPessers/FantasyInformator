
using FantasyInformator.Models.Player;
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.Nfl;

public class Game : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public Guid SeasonId { get; set; }

    public Season? Season { get; set; }

    public int Week { get; set; }

    public Guid HomeNflTeamId { get; set; }

    public NflTeam? HomeNflTeam { get; set; }

    public Guid AwayNflTeamId { get; set; }

    public NflTeam? AwayNflTeam { get; set; }

    public DateTime KickoffUtc { get; set; }

    public List<GameAppearance> GameAppearances { get; init; } = [];
}
