using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FantasyInformator.Shared.Progress;

/// <summary>
/// What happened during an action, for the user and for the log. All reports of one action carry the same
/// <see cref="TraceId"/>, so they can be shown together.
/// </summary>
public record ProgressReport(
    LogLevel Severity,
    string Header,
    string Message,
    bool ShowToUser,
    Exception? Exception)
{
    /// <summary>
    /// When the report was made. A test that checks the time sets its own.
    /// </summary>
    public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// The trace of the action that was running when the report was made, or <see langword="null"/> when none was.
    /// </summary>
    public ActivityTraceId? TraceId { get; } = Activity.Current?.TraceId;
}
