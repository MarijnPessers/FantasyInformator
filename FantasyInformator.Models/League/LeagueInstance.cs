
using FantasyInformator.Models.Lineup;
using FantasyInformator.Models.Nfl;
using FantasyInformator.Models.Scoring;
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.League;

/// <summary>A league in one season. This is the one that has the Sleeper league id.</summary>
public class LeagueInstance : TrackedModel
{
    public string ExternalId { get; set; } = string.Empty;

    public Guid LeagueId { get; set; }

    public League? League { get; set; }

    public Guid SeasonId { get; set; }

    public Season? Season { get; set; }

    public List<LeagueSlot> LeagueSlots { get; init; } = [];

    public List<FantasyTeam.FantasyTeam> FantasyTeams { get; init; } = [];

    public List<FantasyScore> FantasyScores { get; init; } = [];

    public List<PlayerSeasonStats> PlayerSeasonStats { get; init; } = [];
}
