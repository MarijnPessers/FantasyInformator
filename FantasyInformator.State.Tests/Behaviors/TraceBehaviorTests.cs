using System.Diagnostics;
using System.Reflection;
using AutoFixture;
using FantasyInformator.Database.Models.League;
using FantasyInformator.State.Behaviors;
using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;
using static FantasyInformator.State.Store.LeagueState;

namespace FantasyInformator.State.Tests.Behaviors;

[TestClass]
public class TraceBehaviorTests
{
    private readonly Fixture _fixture = new();
    private readonly TraceBehavior<AddLeagueAction, Unit> _behavior = new();

    public TestContext TestContext { get; set; } = null!;

    [TestMethod]
    public void TraceBehavior_ShouldWrapEveryAction_WhenTheStateProjectIsUsed()
    {
        var declared = typeof(LeagueState).Assembly.GetCustomAttributes<MediatorBehaviorAttribute>()
            .Any(attribute => attribute.BehaviorType == typeof(TraceBehavior<,>)
                && attribute.Scope == typeof(ClientPipeline));

        Assert.IsTrue(declared);
    }

    [TestMethod]
    public async Task Handle_ShouldRunTheHandlerInAnActivityNamedAfterTheAction_WhenCalledAsync()
    {
        Activity? running = null;

        _ = await _behavior.Handle(
            CreateAction(),
            _ =>
            {
                running = Activity.Current;
                return Unit.Task;
            },
            TestContext.CancellationToken);

        Assert.AreEqual(nameof(AddLeagueAction), running?.OperationName);
    }

    [TestMethod]
    public async Task Handle_ShouldStartANewTrace_WhenNoActionRunsAsync()
    {
        using var connection = new Activity(_fixture.Create<string>()).Start();
        Activity? running = null;

        _ = await _behavior.Handle(
            CreateAction(),
            _ =>
            {
                running = Activity.Current;
                return Unit.Task;
            },
            TestContext.CancellationToken);

        Assert.AreNotEqual(connection.TraceId, running?.TraceId);
    }

    [TestMethod]
    public async Task Handle_ShouldGiveEachActionItsOwnTrace_WhenTwoRunSideBySideAsync()
    {
        using var connection = new Activity(_fixture.Create<string>()).Start();
        var bothStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = 0;
        var traceIds = new ActivityTraceId?[2];

        // Each action waits until the other one has started too, so both run at the same time.
        RequestHandlerDelegate<Unit> HoldUntilBothStarted(int index) => async _ =>
        {
            traceIds[index] = Activity.Current?.TraceId;
            if (Interlocked.Increment(ref started) == 2)
            {
                bothStarted.SetResult();
            }

            await bothStarted.Task;
            return Unit.Value;
        };

        _ = await Task.WhenAll(
            _behavior.Handle(CreateAction(), HoldUntilBothStarted(0), TestContext.CancellationToken),
            _behavior.Handle(CreateAction(), HoldUntilBothStarted(1), TestContext.CancellationToken));

        Assert.AreNotEqual(traceIds[0], traceIds[1]);
    }

    [TestMethod]
    public async Task Handle_ShouldStayInTheTraceOfTheRunningAction_WhenAnotherActionRunsAsync()
    {
        Activity? outer = null;
        Activity? inner = null;

        _ = await _behavior.Handle(
            CreateAction(),
            async _ =>
            {
                outer = Activity.Current;
                return await _behavior.Handle(
                    CreateAction(),
                    _ =>
                    {
                        inner = Activity.Current;
                        return Unit.Task;
                    },
                    TestContext.CancellationToken);
            },
            TestContext.CancellationToken);

        Assert.AreEqual(outer?.TraceId, inner?.TraceId);
    }

    private AddLeagueAction CreateAction() => new(new League { Name = _fixture.Create<string>() });
}
