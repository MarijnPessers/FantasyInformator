using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class SpinnerState : State<SpinnerState>
{
    public bool IsSpinnerShown { get; private set; } = true;

    public override void Initialize() =>
        IsSpinnerShown = true;
}
