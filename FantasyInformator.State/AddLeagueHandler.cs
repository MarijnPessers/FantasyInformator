using FantasyInformator.Domain.League;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State;

public sealed partial class LeagueListState
{
    public record AddLeagueAction(League League) : IAction;

    public class AddLeagueHandler(IStore store) : ActionHandler<AddLeagueAction>
    {
        private LeagueListState LeagueListState => store.GetState<LeagueListState>();

        public override ValueTask Handle(AddLeagueAction action, CancellationToken cancellationToken)
        {
            LeagueListState.Leagues.Add(action.League);

            return ValueTask.CompletedTask;
        }
    }
}
