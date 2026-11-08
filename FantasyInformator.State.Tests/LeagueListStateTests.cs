using AutoFixture;
using FantasyInformator.Models.League;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.State;

namespace FantasyInformator.State.Tests;

[TestClass]
public class LeagueListStateTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public async Task AddLeagueAction_WithoutBlazor_AddsTheLeagueToTheStateAsync()
    {
        var services = new ServiceCollection();
        _ = services.AddTimeWarpState(x => x.Assemblies = [typeof(LeagueListState).Assembly]);
        await using var serviceProvider = services.BuildServiceProvider();
        var league = new League { Name = _fixture.Create<string>() };
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var store = serviceProvider.GetRequiredService<IStore>();

        await mediator.Send(new LeagueListState.AddLeagueAction(league));

        Assert.AreEqual(league.Name, store.GetState<LeagueListState>().Leagues.Single().Name);
    }
}
