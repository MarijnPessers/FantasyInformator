namespace FantasyInformator.State.Enums;

public enum DatabaseWakeUpStatus
{
    /// <summary>The wake-up call hasn't started yet.</summary>
    NotStarted,

    /// <summary>The wake-up call is running and hasn't answered yet.</summary>
    Waiting,

    /// <summary>The database answered.</summary>
    Awake,

    /// <summary>The database gave an error or didn't answer in time.</summary>
    Unavailable,
}
