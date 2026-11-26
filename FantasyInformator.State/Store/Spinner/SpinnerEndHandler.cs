using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class SpinnerState
{
    public record SpinnerEndAction(Guid SpinnerId) : IAction;

    public class SpinnerEndHandler(IStore store) : ActionHandler<SpinnerEndAction>
    {
        private SpinnerState SpinnerState => store.GetState<SpinnerState>();

        public override ValueTask Handle(SpinnerEndAction request, CancellationToken cancellationToken)
        {
            _ = SpinnerState.ActiveSpinners.RemoveAll(activeSpinner => activeSpinner.SpinnerId == request.SpinnerId);

            return ValueTask.CompletedTask;
        }
    }
}
