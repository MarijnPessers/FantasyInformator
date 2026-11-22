
using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Scoring;

/// <summary>
/// The accumulated scores of a player in a league instance, up to the latest game played.
/// Calculated by the application from <see cref="FantasyScoreDb"/>, never retrieved from Sleeper.
/// The season comes through the league instance.
/// </summary>
public class PlayerSeasonStatsDb : TrackedModelDb
{
    public Guid PlayerId { get; set; }

    public Players.PlayerDb? Player { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstanceDb? LeagueInstance { get; set; }

    /// <summary>Null when the player has not played a game yet in this season.</summary>
    public decimal? TotalScore { get; set; }

    public int GamesActive { get; set; }

    /// <summary>The average over all active games.</summary>
    public decimal? AverageScore { get; set; }

    /// <summary>The average over the last 3 active games.</summary>
    public decimal? AverageScoreLast3 { get; set; }

    /// <summary>The median over all active games.</summary>
    public decimal? MedianScore { get; set; }
}
