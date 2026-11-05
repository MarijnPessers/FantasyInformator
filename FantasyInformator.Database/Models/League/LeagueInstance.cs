namespace FantasyInformator.Database.Models;

/// <summary>A league in one season. This is the one that has the Sleeper league id.</summary>
public class LeagueInstance : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public Guid LeagueId { get; set; }

    public League? League { get; set; }

    public Guid SeasonId { get; set; }

    public Season? Season { get; set; }

    public List<LeagueSlot> LeagueSlots { get; set; } = [];

    public List<FantasyTeam> FantasyTeams { get; set; } = [];

    public List<FantasyScore> FantasyScores { get; set; } = [];

    public List<PlayerSeasonStats> PlayerSeasonStats { get; set; } = [];
}
