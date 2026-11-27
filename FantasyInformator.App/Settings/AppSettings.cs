using FantasyInformator.Shared.Progress;

namespace FantasyInformator.App.Settings;

/// <summary>
/// The settings the App needs, read from the <see cref="SectionName"/> section.
/// </summary>
public class AppSettings
{
    public const string SectionName = "App";

    /// <summary>
    /// The lowest severity a progress report needs to show as a toast. A report marked to show to the user shows
    /// anyway. Success by default.
    /// </summary>
    public MessageLevel DisplayLevel { get; set; } = MessageLevel.Success;

    /// <summary>How long a toast stays before it disappears by itself. Five seconds by default.</summary>
    public TimeSpan ToastDuration { get; set; } = TimeSpan.FromSeconds(5);
}
