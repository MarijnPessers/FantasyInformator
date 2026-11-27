using FantasyInformator.Shared.Progress;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class ProgressReportState : State<ProgressReportState>
{
    /// <summary>Every progress report of this user session, oldest first.</summary>
    public List<ProgressReport> ProgressReports { get; private set; } = [];

    public override void Initialize() =>
        ProgressReports = [];
}
