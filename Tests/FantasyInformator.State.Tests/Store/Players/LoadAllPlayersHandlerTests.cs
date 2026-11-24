using AutoFixture;
using FantasyInformator.Domain.Enums;
using FantasyInformator.Logic.Services.Players;
using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.PlayerState;
using AppPlayer = FantasyInformator.Domain.Players;

namespace FantasyInformator.State.Tests.Store.Players;

[TestClass]
public class LoadAllPlayersHandlerTests
{
    private Fixture _fixture = null!;
    private IPlayerLogic _playerLogic = null!;
    private ServiceProvider _serviceProvider = null!;
    private ISender<ClientPipeline> _sender = null!;
    private IStore _store = null!;

    public TestContext TestContext { get; set; } = null!;

    [TestInitialize]
    public void Initialize()
    {
        _fixture = new Fixture();
        _playerLogic = Substitute.For<IPlayerLogic>();
        _serviceProvider = CreateServices();
        _sender = _serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        _store = _serviceProvider.GetRequiredService<IStore>();
    }

    [TestCleanup]
    public async Task Cleanup() =>
        await _serviceProvider.DisposeAsync();

    [TestMethod]
    public async Task Handle_ShouldReplaceThePlayers_WhenOverrideIsTrueAsync()
    {
        var oldPlayers = CreatePlayers();
        var newPlayers = CreatePlayers();
        _ = _playerLogic.LoadAllPlayersAsync(Arg.Any<CancellationToken>()).Returns(oldPlayers, newPlayers);

        await _sender.Send(new LoadAllPlayersAction(), TestContext.CancellationToken);
        await _sender.Send(new LoadAllPlayersAction(Force: true), TestContext.CancellationToken);

        Assert.AreSequenceEqual(
            newPlayers.Select(player => player.ExternalId).ToList(),
            [.. _store.GetState<PlayerState>().Players.Select(player => player.ExternalId)]);
    }

    [TestMethod]
    public async Task Handle_ShouldNotAskLogic_WhenTheStateAlreadyHoldsPlayersAsync()
    {
        _ = _playerLogic.LoadAllPlayersAsync(Arg.Any<CancellationToken>()).Returns(CreatePlayers());

        await _sender.Send(new LoadAllPlayersAction(), TestContext.CancellationToken);
        await _sender.Send(new LoadAllPlayersAction(), TestContext.CancellationToken);

        _ = _playerLogic.Received(1).LoadAllPlayersAsync(Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task Handle_ShouldSetEveryPlayerToUnchanged_WhenLogicReturnsPlayersAsync()
    {
        _ = _playerLogic.LoadAllPlayersAsync(Arg.Any<CancellationToken>()).Returns(CreatePlayers());

        await _sender.Send(new LoadAllPlayersAction(), TestContext.CancellationToken);

        Assert.IsTrue(_store.GetState<PlayerState>().Players.TrueForAll(player => player.State == TrackingState.Unchanged));
    }

    [TestMethod]
    public async Task Handle_ShouldKeepThePlayers_WhenLogicReturnsNullAsync()
    {
        var oldPlayers = CreatePlayers();
        _ = _playerLogic.LoadAllPlayersAsync(Arg.Any<CancellationToken>())
            .Returns(oldPlayers, (List<AppPlayer.Player>?)null);

        await _sender.Send(new LoadAllPlayersAction(), TestContext.CancellationToken);
        await _sender.Send(new LoadAllPlayersAction(Force: true), TestContext.CancellationToken);

        Assert.AreSequenceEqual(
            oldPlayers.Select(player => player.ExternalId).ToList(),
            [.. _store.GetState<PlayerState>().Players.Select(player => player.ExternalId)]);
    }

    private ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();
        _ = services.AddGeneratedMediator<ClientPipeline>();
        _ = services.AddTimeWarpState(x =>
        {
            x.Assemblies = [typeof(PlayerState).Assembly];

            // TimeWarp copies the state before each action with AnyClone, whose shared lookup table breaks when
            // tests running in parallel fill it at the same time.
            x.UseStateTransactionBehavior = false;
        });
        _ = services.AddSingleton(_playerLogic);

        return services.BuildServiceProvider();
    }

    private List<AppPlayer.Player> CreatePlayers() =>
    [
        new() { ExternalId = _fixture.Create<string>() },
        new() { ExternalId = _fixture.Create<string>() },
        new() { ExternalId = _fixture.Create<string>() },
    ];
}
