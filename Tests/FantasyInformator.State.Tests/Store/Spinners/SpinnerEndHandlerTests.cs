using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.SpinnerState;

namespace FantasyInformator.State.Tests.Store.Spinners;

[TestClass]
public class SpinnerEndHandlerTests
{
    private const string DatabaseMessage = "Connecting to database";
    private const string TestMessage = "This is a test";

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
    public async Task Handle_ShouldHideTheSpinner_WhenTheOnlyActiveSpinnerEndsAsync()
    {
        var spinnerId = Guid.NewGuid();
        await _sender.Send(new SpinnerStartAction(spinnerId, DatabaseMessage), TestContext.CancellationToken);

        await _sender.Send(new SpinnerEndAction(spinnerId), TestContext.CancellationToken);

        Assert.IsFalse(_store.GetState<SpinnerState>().IsSpinnerShown);
    }

    [TestMethod]
    public async Task Handle_ShouldShowTheOtherMessage_WhenOneOfTwoActiveSpinnersEndsAsync()
    {
        var databaseSpinnerId = Guid.NewGuid();
        var testSpinnerId = Guid.NewGuid();
        await _sender.Send(new SpinnerStartAction(databaseSpinnerId, DatabaseMessage), TestContext.CancellationToken);
        await _sender.Send(new SpinnerStartAction(testSpinnerId, TestMessage), TestContext.CancellationToken);

        await _sender.Send(new SpinnerEndAction(databaseSpinnerId), TestContext.CancellationToken);

        Assert.AreEqual(TestMessage, _store.GetState<SpinnerState>().ShownSpinner?.Message);
    }

    private static ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();
        _ = services.AddGeneratedMediator<ClientPipeline>();
        _ = services.AddTimeWarpState(x =>
        {
            x.Assemblies = [typeof(SpinnerState).Assembly];
            x.UseStateTransactionBehavior = false;
        });

        return services.BuildServiceProvider();
    }
}
