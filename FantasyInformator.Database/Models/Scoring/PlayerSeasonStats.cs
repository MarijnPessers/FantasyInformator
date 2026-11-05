namespace FantasyInformator.Database.Models;

/// <summary>
/// The accumulated scores of a player in a league instance, up to the latest game played.
/// Calculated by the application from <see cref="FantasyScore"/>, never retrieved from Sleeper.
/// The season comes through the league instance.
/// </summary>
public class PlayerSeasonStats : TrackedModel
{
    public Guid PlayerId { get; set; }

    public Player? Player { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

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
