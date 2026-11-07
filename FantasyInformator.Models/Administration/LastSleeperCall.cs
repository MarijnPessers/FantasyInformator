namespace FantasyInformator.Models.Administration;

/// <summary>
/// The last time a call to Sleeper was made. One row per call, so a call that Sleeper
/// allows only once a day, such as the players list, can be locked until it is allowed again.
/// </summary>
public class LastSleeperCall
{
    /// <summary>The name of the call, such as the players list. This is the key.</summary>
    public string CallName { get; set; } = string.Empty;

    public DateTime CalledAtUtc { get; set; }
}
