using FantasyInformator.App.Resources;
using FantasyInformator.State.Store;
using TimeWarp.State;

namespace FantasyInformator.App.Components.Layout;

public partial class Spinner : TimeWarpStateComponent
{
    private bool IsSpinnerShown => SpinnerState.IsSpinnerShown;

    private string SpinnerMessage => SpinnerState.ShownSpinner?.Message ?? Texts.DefaultSpinnerMessage;

    private SpinnerState SpinnerState => GetState<SpinnerState>();
}
