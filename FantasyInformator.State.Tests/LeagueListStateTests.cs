using AutoFixture;
using FantasyInformator.Domain.League;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.Mediator;
using TimeWarp.State;

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
        _ = services.AddTimeWarpState(x => x.Assemblies = [typeof(LeagueListState).Assembly]);
        await using var serviceProvider = services.BuildServiceProvider();
        var league = new League { Name = _fixture.Create<string>() };
        var sender = serviceProvider.GetRequiredService<ISender>();
        var store = serviceProvider.GetRequiredService<IStore>();

        await sender.Send(new LeagueListState.AddLeagueAction(league), TestContext.CancellationToken);

        Assert.AreEqual(league.Name, store.GetState<LeagueListState>().Leagues.Single().Name);
    }
}
