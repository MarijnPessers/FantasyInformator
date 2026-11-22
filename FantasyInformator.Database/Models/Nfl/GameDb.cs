using FantasyInformator.Database.Models.Players;

namespace FantasyInformator.Database.Models.Nfl;

public class GameDb : BaseEntityDb
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
