using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.SpinnerState;

namespace FantasyInformator.State.Tests.Store.Spinners;

[TestClass]
public class SpinnerStartHandlerTests
{
    private const string Message = "Connecting to database";

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
    public async Task Handle_ShouldShowTheSpinner_WhenTheSpinnerWasHiddenAsync()
    {
        await _sender.Send(new SpinnerEndAction(), TestContext.CancellationToken);

        await _sender.Send(new SpinnerStartAction(Message), TestContext.CancellationToken);

        Assert.IsTrue(_store.GetState<SpinnerState>().IsSpinnerShown);
    }

    [TestMethod]
    public async Task Handle_ShouldSetTheMessage_WhenAMessageIsGivenAsync()
    {
        await _sender.Send(new SpinnerStartAction(Message), TestContext.CancellationToken);

        Assert.AreEqual(Message, _store.GetState<SpinnerState>().SpinnerMessage);
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
