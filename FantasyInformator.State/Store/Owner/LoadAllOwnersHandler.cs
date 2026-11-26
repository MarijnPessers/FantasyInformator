using FantasyInformator.Logic.Services.FantasyTeams;
using FantasyInformator.State.Settings;
using Microsoft.Extensions.Options;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class OwnerState
{
    public record LoadAllOwnersAction : IAction;

    public class LoadAllOwnersHandler(IStore store, IOwnerLogic ownerLogic, IOptions<StateSettings> settings)
        : ActionHandler<LoadAllOwnersAction>
    {
        private OwnerState OwnerState => store.GetState<OwnerState>();

        public override async ValueTask Handle(LoadAllOwnersAction request, CancellationToken cancellationToken)
        {
            var owners = await ownerLogic.LoadAllOwnersAsync(cancellationToken);

            // On null, Logic has already reported the failure, and the owners already held stay.
            if (owners is not null)
            {
                OwnerState.Owners = owners;
                OwnerState.FocusOwner = owners.Find(owner => owner.Name == settings.Value.FocusOwnerName);
            }
        }
    }
}
