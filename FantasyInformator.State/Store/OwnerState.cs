using FantasyInformator.Domain.FantasyTeams;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class OwnerState : State<OwnerState>
{
    public List<Owner> Owners { get; private set; } = [];

    /// <summary>The owner whose name is the focus owner name in the settings. Null when no owner has that name.</summary>
    public Owner? FocusOwner { get; private set; }

    public override void Initialize()
    {
        Owners = [];
        FocusOwner = null;
    }
}
