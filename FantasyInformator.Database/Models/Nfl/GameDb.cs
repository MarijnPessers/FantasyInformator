
using FantasyInformator.Database.Models.Players;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Nfl;

public class GameDb : TrackedModelDb
{
    public required string ExternalId { get; set; }

    public Guid SeasonId { get; set; }

    public SeasonDb? Season { get; set; }

    public int Week { get; set; }

    public Guid HomeNflTeamId { get; set; }

    public NflTeamDb? HomeNflTeam { get; set; }

    public Guid AwayNflTeamId { get; set; }

    public NflTeamDb? AwayNflTeam { get; set; }

    public DateTime KickoffUtc { get; set; }

    public List<GameAppearanceDb>? GameAppearances { get; init; }
}
