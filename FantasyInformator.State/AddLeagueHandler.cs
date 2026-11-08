using FantasyInformator.Models.League;
using TimeWarp.State;

namespace FantasyInformator.State;

public sealed partial class LeagueListState
{
    public record AddLeagueAction(League League) : IAction;

    public class AddLeagueHandler(IStore store) : ActionHandler<AddLeagueAction>(store)
    {
        private LeagueListState LeagueListState => Store.GetState<LeagueListState>();

        public override Task Handle(AddLeagueAction action, CancellationToken cancellationToken)
        {
            LeagueListState.Leagues.Add(action.League);

            return Task.CompletedTask;
        }
    }
}
