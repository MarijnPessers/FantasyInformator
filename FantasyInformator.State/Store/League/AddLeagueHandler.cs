using FantasyInformator.Database.Models.League;
using TimeWarp.Mediator;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public partial class LeagueState
{
    public record AddLeagueAction(League League) : IAction;

    public class AddLeagueHandler(IStore store) : ActionHandler<AddLeagueAction>
    {
        private LeagueState LeagueListState => store.GetState<LeagueState>();

        public override ValueTask Handle(AddLeagueAction request, CancellationToken cancellationToken)
        {
            LeagueListState.Leagues.Add(request.League);

            return ValueTask.CompletedTask;
        }
    }
}