namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// The line of one player in one game, from the unofficial weekly stats call.
/// Seen in a live response, not in the Sleeper docs. Holds only the fields the application uses.
/// </summary>
public class StatLine
{
    public string PlayerId { get; set; } = string.Empty;

    /// <summary>The same id as <see cref="Game.GameId"/>.</summary>
    public string GameId { get; set; } = string.Empty;

    /// <summary>The team the player played for in this game.</summary>
    public string Team { get; set; } = string.Empty;
}
