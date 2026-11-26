using FantasyInformator.State.Store;
using TimeWarp.State;

namespace FantasyInformator.App.Components.Layout;

public partial class Spinner : TimeWarpStateComponent
{
    private bool IsSpinnerShown => SpinnerState.IsSpinnerShown;

    private SpinnerState SpinnerState => GetState<SpinnerState>();
}
