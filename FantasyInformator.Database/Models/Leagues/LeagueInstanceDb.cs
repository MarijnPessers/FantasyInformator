using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Models.Leagues;

/// <summary>A league in one season. This is the one that has the Sleeper league id.</summary>
public class LeagueInstanceDb : BaseEntityDb
{
    public required string ExternalId { get; set; }

    public Guid LeagueId { get; set; }

    public LeagueDb? League { get; set; }

    public Guid SeasonId { get; set; }

    public SeasonDb? Season { get; set; }

    public List<LeagueSlotDb>? LeagueSlots { get; init; }

    public List<FantasyTeamDb>? FantasyTeams { get; init; }

    public List<FantasyScoreDb>? FantasyScores { get; init; }

    public List<PlayerSeasonStatsDb>? PlayerSeasonStats { get; init; }
}
