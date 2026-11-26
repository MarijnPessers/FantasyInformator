using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class SpinnerState
{
    public record SpinnerEndAction : IAction;

    public class SpinnerEndHandler(IStore store) : ActionHandler<SpinnerEndAction>
    {
        private SpinnerState SpinnerState => store.GetState<SpinnerState>();

        public override ValueTask Handle(SpinnerEndAction request, CancellationToken cancellationToken)
        {
            SpinnerState.IsSpinnerShown = false;

            return ValueTask.CompletedTask;
        }
    }
}
