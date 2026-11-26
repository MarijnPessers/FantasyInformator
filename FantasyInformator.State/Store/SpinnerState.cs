using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class SpinnerState : State<SpinnerState>
{
    public bool IsSpinnerShown { get; private set; } = true;

    /// <summary>The text shown under the spinner. Null when the spinner shows no text.</summary>
    public string? SpinnerMessage { get; private set; }

    public override void Initialize()
    {
        IsSpinnerShown = true;
        SpinnerMessage = null;
    }
}
