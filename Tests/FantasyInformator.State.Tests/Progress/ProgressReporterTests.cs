using FantasyInformator.Shared.Progress;
using FantasyInformator.State.Progress;
using FantasyInformator.State.Store;
using NSubstitute;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Tests.Progress;

[TestClass]
public class ProgressReporterTests
{
    private ISender<ClientPipeline> _sender = null!;
    private ProgressReporter _progressReporter = null!;

    [TestInitialize]
    public void Initialize()
    {
        _sender = Substitute.For<ISender<ClientPipeline>>();
        _progressReporter = new ProgressReporter(_sender);
    }

    [TestMethod]
    public async Task Report_ShouldSendTheAddActionWithTheReport_WhenCalledAsync()
    {
        var report = new ProgressReport(MessageLevel.Error, "Database", "The database isn't available.", true, null);

        _progressReporter.Report(report);

        await _sender.Received(1).Send(
            Arg.Is<ProgressReportState.AddProgressReportAction>(action => action.ProgressReport == report),
            Arg.Any<CancellationToken>());
    }
}
