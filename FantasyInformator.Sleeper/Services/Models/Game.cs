namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>
/// One game of the unofficial schedule call. Seen in a live response, not in the Sleeper docs.
/// Holds only the fields the application uses.
/// </summary>
public class Game
{
    public string GameId { get; set; } = string.Empty;

    public int Week { get; set; }

    /// <summary>The day of the game. The call gives no time of day.</summary>
    public DateOnly Date { get; set; }

    /// <summary>The abbreviation of the home team, for example "KC".</summary>
    public string Home { get; set; } = string.Empty;

    /// <summary>The abbreviation of the away team.</summary>
    public string Away { get; set; } = string.Empty;
}
