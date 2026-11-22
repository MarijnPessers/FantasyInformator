using FantasyInformator.Logic.Services.Players;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class PlayerState
{
    public record FetchAllPlayersAction : IAction;

    public class FetchAllPlayersHandler(IStore store, IPlayerLogic playerLogic) : ActionHandler<FetchAllPlayersAction>
    {
        private PlayerState PlayerState => store.GetState<PlayerState>();

        public override async ValueTask Handle(FetchAllPlayersAction request, CancellationToken cancellationToken)
        {
            var players = await playerLogic.FetchAllPlayersAsync(cancellationToken);

            // On null, Logic has already reported the failure, and the players already shown stay.
            if (players is not null)
            {
                PlayerState.ReplacePlayers(players);
            }
        }
    }
}
