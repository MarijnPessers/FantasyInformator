using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class SpinnerState
{
    public record SpinnerStartAction(string? Message) : IAction;

    public class SpinnerStartHandler(IStore store) : ActionHandler<SpinnerStartAction>
    {
        private SpinnerState SpinnerState => store.GetState<SpinnerState>();

        public override ValueTask Handle(SpinnerStartAction request, CancellationToken cancellationToken)
        {
            SpinnerState.IsSpinnerShown = true;
            SpinnerState.SpinnerMessage = request.Message;

            return ValueTask.CompletedTask;
        }
    }
}
