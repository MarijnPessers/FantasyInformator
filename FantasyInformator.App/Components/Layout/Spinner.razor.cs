using FantasyInformator.State.Store;
using TimeWarp.State;

namespace FantasyInformator.App.Components.Layout;

public partial class Spinner : TimeWarpStateComponent
{
    private bool IsSpinnerShown => SpinnerState.IsSpinnerShown;

    private bool HasSpinnerMessage => !string.IsNullOrEmpty(SpinnerMessage);

    private string? SpinnerMessage => SpinnerState.SpinnerMessage;

    private SpinnerState SpinnerState => GetState<SpinnerState>();
}
