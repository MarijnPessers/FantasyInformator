using FantasyInformator.Logic.Services.Players;
using FantasyInformator.State.Abstractions;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class PlayerState
{
    /// <param name="Force">When true, the players are loaded even when the state already holds players.</param>
    public record LoadAllPlayersAction(bool Force = false) : ISpinnerAction;

    public class LoadAllPlayersHandler(IStore store, IPlayerLogic playerLogic) : ActionHandler<LoadAllPlayersAction>
    {
        private PlayerState PlayerState => store.GetState<PlayerState>();

        public override async ValueTask Handle(LoadAllPlayersAction request, CancellationToken cancellationToken)
        {
            if (!request.Force && PlayerState.Players.Count > 0)
            {
                return;
            }

            var players = await playerLogic.LoadAllPlayersAsync(cancellationToken);

            // On null, Logic has already reported the failure, and the players already shown stay.
            if (players is not null)
            {
                PlayerState.ReplacePlayers(players);
            }
        }
    }
}
