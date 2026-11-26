using FantasyInformator.State.Spinners;
using FantasyInformator.State.Store;
using NSubstitute;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Tests.Spinners;

[TestClass]
public class SpinnerScopeTests
{
    private ISender<ClientPipeline> _sender = null!;

    [TestInitialize]
    public void Initialize() =>
        _sender = Substitute.For<ISender<ClientPipeline>>();

    [TestMethod]
    public async Task DisposeAsync_ShouldEndTheSpinner_WhenDisposedAsync()
    {
        var spinnerId = Guid.NewGuid();
        var spinner = new SpinnerScope(_sender, spinnerId);

        await spinner.DisposeAsync();

        await _sender.Received(1).Send(
            Arg.Is<SpinnerState.SpinnerEndAction>(action => action.SpinnerId == spinnerId),
            Arg.Any<CancellationToken>());
    }
}
