using FantasyInformator.Logic.Services.Databases;
using FantasyInformator.State.Enums;
using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.DatabaseState;

namespace FantasyInformator.State.Tests.Store.Databases;

[TestClass]
public class DatabaseWakeUpHandlerTests
{
    private IDatabaseWakeUpLogic _databaseWakeUpLogic = null!;
    private ServiceProvider _serviceProvider = null!;
    private ISender<ClientPipeline> _sender = null!;
    private IStore _store = null!;

    public TestContext TestContext { get; set; } = null!;

    [TestInitialize]
    public void Initialize()
    {
        _databaseWakeUpLogic = Substitute.For<IDatabaseWakeUpLogic>();
        _serviceProvider = CreateServices();
        _sender = _serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        _store = _serviceProvider.GetRequiredService<IStore>();
    }

    [TestCleanup]
    public async Task Cleanup() =>
        await _serviceProvider.DisposeAsync();

    [TestMethod]
    public void DatabaseWakeUpStatus_ShouldBeNotStarted_WhenNothingIsSentYet() =>
        Assert.AreEqual(DatabaseWakeUpStatus.NotStarted, _store.GetState<DatabaseState>().DatabaseWakeUpStatus);

    [TestMethod]
    public async Task Handle_ShouldSetAwake_WhenTheDatabaseAnswersAsync()
    {
        _ = _databaseWakeUpLogic.WakeUpAsync(Arg.Any<CancellationToken>()).Returns(true);

        await _sender.Send(new DatabaseWakeUpAction(), TestContext.CancellationToken);

        Assert.AreEqual(DatabaseWakeUpStatus.Awake, _store.GetState<DatabaseState>().DatabaseWakeUpStatus);
    }

    [TestMethod]
    public async Task Handle_ShouldSetUnavailable_WhenTheDatabaseDoesNotAnswerAsync()
    {
        _ = _databaseWakeUpLogic.WakeUpAsync(Arg.Any<CancellationToken>()).Returns(false);

        await _sender.Send(new DatabaseWakeUpAction(), TestContext.CancellationToken);

        Assert.AreEqual(DatabaseWakeUpStatus.Unavailable, _store.GetState<DatabaseState>().DatabaseWakeUpStatus);
    }

    private ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();
        _ = services.AddGeneratedMediator<ClientPipeline>();
        _ = services.AddTimeWarpState(x =>
        {
            x.Assemblies = [typeof(DatabaseState).Assembly];

            // TimeWarp copies the state before each action with AnyClone, whose shared lookup table breaks when
            // tests running in parallel fill it at the same time.
            x.UseStateTransactionBehavior = false;
        });
        _ = services.AddSingleton(_databaseWakeUpLogic);

        return services.BuildServiceProvider();
    }
}
