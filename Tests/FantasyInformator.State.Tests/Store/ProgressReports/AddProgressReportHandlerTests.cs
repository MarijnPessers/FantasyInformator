using FantasyInformator.Shared.Progress;
using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.ProgressReportState;

namespace FantasyInformator.State.Tests.Store.ProgressReports;

[TestClass]
public class AddProgressReportHandlerTests
{
    private ServiceProvider _serviceProvider = null!;
    private ISender<ClientPipeline> _sender = null!;
    private IStore _store = null!;

    public TestContext TestContext { get; set; } = null!;

    [TestInitialize]
    public void Initialize()
    {
        _serviceProvider = CreateServices();
        _sender = _serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        _store = _serviceProvider.GetRequiredService<IStore>();
    }

    [TestCleanup]
    public async Task Cleanup() =>
        await _serviceProvider.DisposeAsync();

    [TestMethod]
    public async Task Handle_ShouldAddTheReportAfterTheEarlierOnes_WhenReportsWereAddedBeforeAsync()
    {
        var firstReport = new ProgressReport(MessageLevel.Information, "Players", "Players loaded.", false, null);
        var secondReport = new ProgressReport(MessageLevel.Error, "Database", "Saving failed.", true, null);
        await _sender.Send(new AddProgressReportAction(firstReport), TestContext.CancellationToken);

        await _sender.Send(new AddProgressReportAction(secondReport), TestContext.CancellationToken);

        Assert.AreSequenceEqual(
            [firstReport, secondReport], _store.GetState<ProgressReportState>().ProgressReports);
    }

    private static ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();
        _ = services.AddGeneratedMediator<ClientPipeline>();
        _ = services.AddTimeWarpState(x =>
        {
            x.Assemblies = [typeof(ProgressReportState).Assembly];
            x.UseStateTransactionBehavior = false;
        });

        return services.BuildServiceProvider();
    }
}
