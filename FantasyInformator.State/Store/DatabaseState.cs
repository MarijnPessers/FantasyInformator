using FantasyInformator.State.Enums;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class DatabaseState : State<DatabaseState>
{
    public DatabaseWakeUpStatus DatabaseWakeUpStatus { get; private set; } = DatabaseWakeUpStatus.NotStarted;

    public override void Initialize() =>
        DatabaseWakeUpStatus = DatabaseWakeUpStatus.NotStarted;
}
