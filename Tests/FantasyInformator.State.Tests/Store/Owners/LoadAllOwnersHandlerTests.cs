using AutoFixture;
using FantasyInformator.Domain.FantasyTeams;
using FantasyInformator.Logic.Services.FantasyTeams;
using FantasyInformator.State.Settings;
using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.OwnerState;

namespace FantasyInformator.State.Tests.Store.Owners;

[TestClass]
public class LoadAllOwnersHandlerTests
{
    private const string FocusOwnerName = "GoudaCheesewizz";

    private Fixture _fixture = null!;
    private IOwnerLogic _ownerLogic = null!;
    private ServiceProvider _serviceProvider = null!;
    private ISender<ClientPipeline> _sender = null!;
    private IStore _store = null!;

    public TestContext TestContext { get; set; } = null!;

    [TestInitialize]
    public void Initialize()
    {
        _fixture = new Fixture();
        _ownerLogic = Substitute.For<IOwnerLogic>();
        _serviceProvider = CreateServices();
        _sender = _serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        _store = _serviceProvider.GetRequiredService<IStore>();
    }

    [TestCleanup]
    public async Task Cleanup() =>
        await _serviceProvider.DisposeAsync();

    [TestMethod]
    public async Task Handle_ShouldOverrideTheOwners_WhenCalledAgainAsync()
    {
        var oldOwners = CreateOwners();
        var newOwners = CreateOwners();
        _ = _ownerLogic.LoadAllOwnersAsync(Arg.Any<CancellationToken>()).Returns(oldOwners, newOwners);

        await _sender.Send(new LoadAllOwnersAction(), TestContext.CancellationToken);
        await _sender.Send(new LoadAllOwnersAction(), TestContext.CancellationToken);

        Assert.AreSequenceEqual(
            newOwners.Select(owner => owner.ExternalId).ToList(),
            [.. _store.GetState<OwnerState>().Owners.Select(owner => owner.ExternalId)]);
    }

    [TestMethod]
    public async Task Handle_ShouldKeepTheOwners_WhenLogicReturnsNullAsync()
    {
        var oldOwners = CreateOwners();
        _ = _ownerLogic.LoadAllOwnersAsync(Arg.Any<CancellationToken>()).Returns(oldOwners, (List<Owner>?)null);

        await _sender.Send(new LoadAllOwnersAction(), TestContext.CancellationToken);
        await _sender.Send(new LoadAllOwnersAction(), TestContext.CancellationToken);

        Assert.AreSequenceEqual(
            oldOwners.Select(owner => owner.ExternalId).ToList(),
            [.. _store.GetState<OwnerState>().Owners.Select(owner => owner.ExternalId)]);
    }

    [TestMethod]
    public async Task Handle_ShouldSetTheFocusOwner_WhenAnOwnerHasTheFocusOwnerNameAsync()
    {
        var owners = CreateOwners();
        owners[1].Name = FocusOwnerName;
        _ = _ownerLogic.LoadAllOwnersAsync(Arg.Any<CancellationToken>()).Returns(owners);

        await _sender.Send(new LoadAllOwnersAction(), TestContext.CancellationToken);

        Assert.AreSame(owners[1], _store.GetState<OwnerState>().FocusOwner);
    }

    [TestMethod]
    public async Task Handle_ShouldLeaveTheFocusOwnerEmpty_WhenNoOwnerHasTheFocusOwnerNameAsync()
    {
        _ = _ownerLogic.LoadAllOwnersAsync(Arg.Any<CancellationToken>()).Returns(CreateOwners());

        await _sender.Send(new LoadAllOwnersAction(), TestContext.CancellationToken);

        Assert.IsNull(_store.GetState<OwnerState>().FocusOwner);
    }

    private ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();
        _ = services.AddGeneratedMediator<ClientPipeline>();
        _ = services.AddTimeWarpState(x =>
        {
            x.Assemblies = [typeof(OwnerState).Assembly];

            // TimeWarp copies the state before each action with AnyClone, whose shared lookup table breaks when
            // tests running in parallel fill it at the same time.
            x.UseStateTransactionBehavior = false;
        });
        _ = services.AddSingleton(_ownerLogic);
        _ = services.AddSingleton(Options.Create(new StateSettings { FocusOwnerName = FocusOwnerName }));

        return services.BuildServiceProvider();
    }

    private List<Owner> CreateOwners() =>
    [
        new() { ExternalId = _fixture.Create<string>() },
        new() { ExternalId = _fixture.Create<string>() },
        new() { ExternalId = _fixture.Create<string>() },
    ];
}
