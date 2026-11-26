using System.Reflection;
using FantasyInformator.State.Abstractions;
using FantasyInformator.State.Behaviors;
using FantasyInformator.State.Store;
using NSubstitute;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Tests.Behaviors;

[TestClass]
public class SpinnerBehaviorTests
{
    private const string Message = "Connecting to database";

    private ISender<ClientPipeline> _sender = null!;

    public TestContext TestContext { get; set; } = null!;

    [TestInitialize]
    public void Initialize() =>
        _sender = Substitute.For<ISender<ClientPipeline>>();

    [TestMethod]
    public void SpinnerBehavior_ShouldWrapEveryAction_WhenTheStateProjectIsUsed()
    {
        var declared = typeof(SpinnerBehavior<,>).Assembly.GetCustomAttributes<MediatorBehaviorAttribute>()
            .Any(attribute => attribute.BehaviorType == typeof(SpinnerBehavior<,>)
                && attribute.Scope == typeof(ClientPipeline));

        Assert.IsTrue(declared);
    }

    [TestMethod]
    public async Task Handle_ShouldStartASpinnerWithTheActionMessage_WhenTheActionIsASpinnerActionAsync()
    {
        var behavior = new SpinnerBehavior<SampleState.SpinnerSampleAction, Unit>(_sender);

        _ = await behavior.Handle(new SampleState.SpinnerSampleAction(Message), _ => Unit.Task,
            TestContext.CancellationToken);

        await _sender.Received(1).Send(
            Arg.Is<SpinnerState.SpinnerStartAction>(action => action.Message == Message),
            Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task Handle_ShouldEndTheSpinner_WhenTheHandlerThrowsAsync()
    {
        var behavior = new SpinnerBehavior<SampleState.SpinnerSampleAction, Unit>(_sender);

        _ = await Assert.ThrowsAsync<InvalidOperationException>(() => behavior.Handle(
            new SampleState.SpinnerSampleAction(Message),
            _ => throw new InvalidOperationException(),
            TestContext.CancellationToken));

        await _sender.Received(1).Send(Arg.Any<SpinnerState.SpinnerEndAction>(), Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task Handle_ShouldSendNoAction_WhenTheActionIsNoSpinnerActionAsync()
    {
        var behavior = new SpinnerBehavior<SampleState.PlainSampleAction, Unit>(_sender);

        _ = await behavior.Handle(new SampleState.PlainSampleAction(), _ => Unit.Task, TestContext.CancellationToken);

        Assert.IsEmpty(_sender.ReceivedCalls());
    }

    private sealed class SampleState : State<SampleState>
    {
        public override void Initialize() { }

        public sealed record SpinnerSampleAction(string? SpinnerMessage) : ISpinnerAction;

        public sealed record PlainSampleAction : IAction;
    }
}
