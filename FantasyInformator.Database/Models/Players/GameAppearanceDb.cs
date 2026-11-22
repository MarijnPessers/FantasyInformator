
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Players;

/// <summary>A player taking part in a game.</summary>
public class GameAppearanceDb : TrackedModelDb
{
    public Guid PlayerId { get; set; }

    public PlayerDb? Player { get; set; }

    public Guid GameId { get; set; }

    public GameDb? Game { get; set; }

    /// <summary>True when the player played for the home team of the game.</summary>
    public bool IsHome { get; set; }

    public List<FantasyScoreDb>? FantasyScores { get; init; }
}
