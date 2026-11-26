using FantasyInformator.Logic.Services.Databases;
using FantasyInformator.State.Abstractions;
using FantasyInformator.State.Enums;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class DatabaseState
{
    public record DatabaseWakeUpAction(string? SpinnerMessage = null) : ISpinnerAction;

    public class DatabaseWakeUpHandler(IStore store, IDatabaseWakeUpLogic databaseWakeUpLogic)
        : ActionHandler<DatabaseWakeUpAction>
    {
        private DatabaseState DatabaseState => store.GetState<DatabaseState>();

        public override async ValueTask Handle(DatabaseWakeUpAction request, CancellationToken cancellationToken)
        {
            DatabaseState.DatabaseWakeUpStatus = DatabaseWakeUpStatus.Waiting;

            var isAwake = await databaseWakeUpLogic.WakeUpAsync(cancellationToken);

            DatabaseState.DatabaseWakeUpStatus = isAwake ? DatabaseWakeUpStatus.Awake : DatabaseWakeUpStatus.Unavailable;
        }
    }
}
