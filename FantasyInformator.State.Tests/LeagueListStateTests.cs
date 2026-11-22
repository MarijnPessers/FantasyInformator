using AutoFixture;
using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.State.Store;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.LeagueState;

namespace FantasyInformator.State.Tests;

[TestClass]
public class LeagueListStateTests
{
    private readonly Fixture _fixture = new();

    public TestContext TestContext { get; set; } = null!;

    [TestMethod]
    public async Task AddLeagueAction_ShouldAddTheLeagueToTheState_WhenSentWithoutBlazorAsync()
    {
        var services = new ServiceCollection();
        _ = services.AddGeneratedMediator<ClientPipeline>();
        _ = services.AddTimeWarpState(x => x.Assemblies = [typeof(LeagueState).Assembly]);
        await using var serviceProvider = services.BuildServiceProvider();
        var league = new League { Name = _fixture.Create<string>() };
        var sender = serviceProvider.GetRequiredService<ISender<ClientPipeline>>();
        var store = serviceProvider.GetRequiredService<IStore>();

        await sender.Send(new AddLeagueAction(league), TestContext.CancellationToken);

        Assert.AreEqual(league.Name, store.GetState<LeagueState>().Leagues.Single().Name);
    }
}
