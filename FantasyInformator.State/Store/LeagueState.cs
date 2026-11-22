using FantasyInformator.Database.Models.Leagues;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class LeagueState : State<LeagueState>
{
    public List<League> Leagues { get; private set; } = [];

    public override void Initialize() =>
        Leagues = [];
}
