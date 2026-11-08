using FantasyInformator.Models.League;
using TimeWarp.State;

namespace FantasyInformator.State;

public sealed partial class LeagueListState : State<LeagueListState>
{
    public List<League> Leagues { get; private set; } = [];

    public override void Initialize()
    {
        Leagues = [];
    }
}
