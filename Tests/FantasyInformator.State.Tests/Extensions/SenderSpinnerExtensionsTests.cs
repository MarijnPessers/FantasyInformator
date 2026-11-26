using FantasyInformator.State.Extensions;
using FantasyInformator.State.Store;
using NSubstitute;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Tests.Extensions;

[TestClass]
public class SenderSpinnerExtensionsTests
{
    private const string Message = "Saving players";

    private ISender<ClientPipeline> _sender = null!;

    public TestContext TestContext { get; set; } = null!;

    [TestInitialize]
    public void Initialize() =>
        _sender = Substitute.For<ISender<ClientPipeline>>();

    [TestMethod]
    public async Task StartSpinnerAsync_ShouldStartASpinnerWithTheMessage_WhenCalledAsync()
    {
        await using var spinner = await _sender.StartSpinnerAsync(Message, TestContext.CancellationToken);

        await _sender.Received(1).Send(
            Arg.Is<SpinnerState.SpinnerStartAction>(action => action.Message == Message),
            Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task StartSpinnerAsync_ShouldNotEndTheSpinner_WhileTheScopeIsNotDisposedAsync()
    {
        await using var spinner = await _sender.StartSpinnerAsync(Message, TestContext.CancellationToken);

        await _sender.DidNotReceive().Send(Arg.Any<SpinnerState.SpinnerEndAction>(), Arg.Any<CancellationToken>());
    }
}
