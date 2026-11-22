
using FantasyInformator.Domain.Lineups;
using FantasyInformator.Domain.Nfl;
using FantasyInformator.Domain.Scoring;
using FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Domain.Leagues;

/// <summary>A league in one season. This is the one that has the Sleeper league id.</summary>
public class LeagueInstance : TrackedModel
{
    public required string ExternalId { get; set; }

    public Guid LeagueId { get; set; }

    public League? League { get; set; }

    public Guid SeasonId { get; set; }

    public Season? Season { get; set; }

    public List<LeagueSlot>? LeagueSlots { get; init; }

    public List<FantasyTeams.FantasyTeam>? FantasyTeams { get; init; }

    public List<FantasyScore>? FantasyScores { get; init; }

    public List<PlayerSeasonStats>? PlayerSeasonStats { get; init; }
}
