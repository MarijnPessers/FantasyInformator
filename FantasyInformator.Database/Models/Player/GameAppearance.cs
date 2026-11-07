
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Player;

/// <summary>A player taking part in a game.</summary>
public class GameAppearance : TrackedModel
{
    public Guid PlayerId { get; set; }

    public Player? Player { get; set; }

    public Guid GameId { get; set; }

    public Game? Game { get; set; }

    /// <summary>True when the player played for the home team of the game.</summary>
    public bool IsHome { get; set; }

    public List<FantasyScore> FantasyScores { get; init; } = [];
}
