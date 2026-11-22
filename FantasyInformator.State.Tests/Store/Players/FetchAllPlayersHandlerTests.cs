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
public class FetchAllPlayersHandlerTests
{
    private readonly Fixture _fixture = new();
    private readonly IPlayerLogic _playerLogic = Substitute.For<IPlayerLogic>();

    public TestContext TestContext { get; set; } = null!;

    [TestMethod]
    public async Task Handle_ShouldReplaceThePlayers_WhenLogicReturnsPlayersAsync()
    {
        var oldPlayers = CreatePlayers();
        var newPlayers = CreatePlayers();
        _ = _playerLogic.FetchAllPlayersAsync(Arg.Any<CancellationToken>()).Returns(oldPlayers, newPlayers);
        await using var serviceProvider = CreateServices();
        var sender = serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        var store = serviceProvider.GetRequiredService<IStore>();

        await sender.Send(new FetchAllPlayersAction(), TestContext.CancellationToken);
        await sender.Send(new FetchAllPlayersAction(), TestContext.CancellationToken);

        Assert.AreSequenceEqual(
            newPlayers.Select(player => player.ExternalId).ToList(),
            [.. store.GetState<PlayerState>().Players.Select(player => player.ExternalId)]);
    }

    [TestMethod]
    public async Task Handle_ShouldSetEveryPlayerToUnchanged_WhenLogicReturnsPlayersAsync()
    {
        _ = _playerLogic.FetchAllPlayersAsync(Arg.Any<CancellationToken>()).Returns(CreatePlayers());
        await using var serviceProvider = CreateServices();
        var sender = serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        var store = serviceProvider.GetRequiredService<IStore>();

        await sender.Send(new FetchAllPlayersAction(), TestContext.CancellationToken);

        Assert.IsTrue(store.GetState<PlayerState>().Players.TrueForAll(player => player.State == TrackingState.Unchanged));
    }

    [TestMethod]
    public async Task Handle_ShouldKeepThePlayers_WhenLogicReturnsNullAsync()
    {
        var oldPlayers = CreatePlayers();
        _ = _playerLogic.FetchAllPlayersAsync(Arg.Any<CancellationToken>())
            .Returns(oldPlayers, (List<AppPlayer.Player>?)null);
        await using var serviceProvider = CreateServices();
        var sender = serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        var store = serviceProvider.GetRequiredService<IStore>();

        await sender.Send(new FetchAllPlayersAction(), TestContext.CancellationToken);
        await sender.Send(new FetchAllPlayersAction(), TestContext.CancellationToken);

        Assert.AreSequenceEqual(
            oldPlayers.Select(player => player.ExternalId).ToList(),
            [.. store.GetState<PlayerState>().Players.Select(player => player.ExternalId)]);
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
