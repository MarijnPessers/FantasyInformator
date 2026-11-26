using FantasyInformator.State.Spinners;
using FantasyInformator.State.Store;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Extensions;

public static class SenderSpinnerExtensions
{
    extension(ISender<ClientPipeline> sender)
    {
        /// <summary>
        /// Starts a spinner with <paramref name="message"/> and returns it as a <see cref="SpinnerScope"/>.
        /// Disposing the scope ends the spinner. The oldest active spinner's message is shown, so spinners started
        /// by actions inside the scope stay behind this message. Null shows the default text.
        /// </summary>
        public async Task<SpinnerScope> StartSpinnerAsync(string? message, CancellationToken cancellationToken)
        {
            var spinnerId = Guid.NewGuid();
            await sender.Send(new SpinnerState.SpinnerStartAction(spinnerId, message), cancellationToken);

            return new SpinnerScope(sender, spinnerId);
        }
    }
}
