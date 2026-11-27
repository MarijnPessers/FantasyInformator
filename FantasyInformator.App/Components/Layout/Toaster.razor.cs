using FantasyInformator.App.Components.Styling;
using FantasyInformator.App.Settings;
using FantasyInformator.Shared.Progress;
using FantasyInformator.State.Store;
using Microsoft.Extensions.Options;
using TimeWarp.State;

namespace FantasyInformator.App.Components.Layout;

public partial class Toaster(IOptions<AppSettings> settings) : TimeWarpStateComponent
{
    private readonly List<Toast> _toasts = [];

    private int _handledReportCount;

    private IReadOnlyList<Toast> Toasts => _toasts;

    private ProgressReportState ProgressReportState => GetState<ProgressReportState>();

    protected override void OnInitialized() =>
        ShowNewReports();

    /// <summary>
    /// Called when a state this component reads has changed. Picks up the new reports before the component renders
    /// again.
    /// </summary>
    public override bool ShouldReRender(Type stateType)
    {
        _ = InvokeAsync(ShowNewReports);

        return base.ShouldReRender(stateType);
    }

    private void RemoveToast(Toast toast) =>
        _ = _toasts.Remove(toast);

    private void ShowNewReports()
    {
        var progressReports = ProgressReportState.ProgressReports;

        foreach (var progressReport in progressReports.Skip(_handledReportCount).Where(IsShownToUser))
        {
            var toast = new Toast(progressReport, progressReport.Severity.ToColorVariant().ToColorClasses());
            _toasts.Add(toast);
            _ = RemoveAfterToastDurationAsync(toast);
        }

        _handledReportCount = progressReports.Count;
    }

    private bool IsShownToUser(ProgressReport progressReport) =>
        progressReport.ShowToUser || progressReport.Severity >= settings.Value.DisplayLevel;

    private async Task RemoveAfterToastDurationAsync(Toast toast)
    {
        try
        {
            await Task.Delay(settings.Value.ToastDuration, CancellationToken);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        await InvokeAsync(() =>
        {
            _ = _toasts.Remove(toast);
            StateHasChanged();
        });
    }

    private sealed class Toast(ProgressReport progressReport, string colorClasses)
    {
        public string Header => progressReport.Header;

        public string Message => progressReport.Message;

        public string ColorClasses => colorClasses;
    }
}
