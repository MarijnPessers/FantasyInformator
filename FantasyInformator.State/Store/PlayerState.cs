using FantasyInformator.Domain.Enums;
using FantasyInformator.Domain.Players;
using TimeWarp.State;

namespace FantasyInformator.State.Store;

public sealed partial class PlayerState : State<PlayerState>
{
    public List<Player> Players { get; private set; } = [];

    public override void Initialize() =>
        Players = [];

    private void ReplacePlayers(List<Player> players)
    {
        // Logic has already saved these players, so nothing about them is left to save.
        players.ForEach(player => player.State = TrackingState.Unchanged);
        Players = players;
    }
}
