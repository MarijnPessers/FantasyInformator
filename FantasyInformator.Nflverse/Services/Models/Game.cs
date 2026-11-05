namespace FantasyInformator.Nflverse.Services.Models;

/// <summary>
/// One row of the nflverse <c>games.csv</c>. Holds only the columns the application uses.
/// </summary>
public class Game
{
    public int Season { get; set; }

    /// <summary>The kind of game: "REG" for the regular season, other values for preseason and postseason.</summary>
    public string GameType { get; set; } = string.Empty;

    public int Week { get; set; }

    /// <summary>The day of the game, as in the <c>gameday</c> column.</summary>
    public DateOnly Gameday { get; set; }

    /// <summary>The kickoff time in Eastern time, 24-hour, whatever the time zone of the stadium (<c>gametime</c> column).</summary>
    public TimeOnly Gametime { get; set; }

    public string AwayTeam { get; set; } = string.Empty;

    public string HomeTeam { get; set; } = string.Empty;
}
