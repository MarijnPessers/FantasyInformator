using FantasyInformator.Shared.Progress;
using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Progress;

/// <summary>
/// Receives the progress reports of every project and adds each one to <see cref="ProgressReportState"/>. Nothing
/// waits for that, so a report may arrive after the call that made it has returned.
/// </summary>
public class ProgressReporter(ISender<ClientPipeline> sender) : IProgress<ProgressReport>
{
    /// <summary>Sends the action that adds <paramref name="value"/> to <see cref="ProgressReportState"/>.</summary>
    public void Report(ProgressReport value) =>
        _ = sender.Send(new ProgressReportState.AddProgressReportAction(value), CancellationToken.None);
}
