namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>The state object of <c>GET /state/nfl</c>. Holds only the fields the application uses.</summary>
public class State
{
    /// <summary>The current NFL season as Sleeper writes it, for example "2025".</summary>
    public string Season { get; set; } = string.Empty;

    public int Week { get; set; }
}
