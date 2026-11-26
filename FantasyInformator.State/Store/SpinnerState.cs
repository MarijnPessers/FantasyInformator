using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class SpinnerState : State<SpinnerState>
{
    /// <summary>The spinners that are started and not ended yet, oldest first.</summary>
    public List<ActiveSpinner> ActiveSpinners { get; private set; } = [];

    public bool IsSpinnerShown => ActiveSpinners.Count > 0;

    /// <summary>The spinner whose message is shown: the one started first. Null when no spinner is active.</summary>
    public ActiveSpinner? ShownSpinner => ActiveSpinners.FirstOrDefault();

    public override void Initialize() =>
        ActiveSpinners = [];

    public record ActiveSpinner(Guid SpinnerId, string? Message);
}
