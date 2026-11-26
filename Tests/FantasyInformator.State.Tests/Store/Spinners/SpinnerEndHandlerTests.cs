using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.SpinnerState;

namespace FantasyInformator.State.Tests.Store.Spinners;

[TestClass]
public class SpinnerEndHandlerTests
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
    public async Task Handle_ShouldHideTheSpinner_WhenTheSpinnerWasShownAsync()
    {
        await _sender.Send(new SpinnerStartAction(Message), TestContext.CancellationToken);

        await _sender.Send(new SpinnerEndAction(), TestContext.CancellationToken);

        Assert.IsFalse(_store.GetState<SpinnerState>().IsSpinnerShown);
    }

    [TestMethod]
    public async Task Handle_ShouldClearTheMessage_WhenTheSpinnerHadAMessageAsync()
    {
        await _sender.Send(new SpinnerStartAction(Message), TestContext.CancellationToken);

        await _sender.Send(new SpinnerEndAction(), TestContext.CancellationToken);

        Assert.IsNull(_store.GetState<SpinnerState>().SpinnerMessage);
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
