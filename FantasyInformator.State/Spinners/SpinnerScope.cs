using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Spinners;

/// <summary>A started spinner. Disposing it ends that spinner.</summary>
public sealed class SpinnerScope(ISender<ClientPipeline> sender, Guid spinnerId) : IAsyncDisposable
{
    /// <summary>Ends the spinner, so it no longer counts as active.</summary>
    public async ValueTask DisposeAsync() =>
        await sender.Send(new SpinnerState.SpinnerEndAction(spinnerId), CancellationToken.None);
}
