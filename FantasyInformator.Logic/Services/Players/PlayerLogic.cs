using AppPlayer = FantasyInformator.Domain.Players;

namespace FantasyInformator.Logic.Services.Players;

public interface IPlayerLogic
{
    /// <summary>
    /// Fetches the players from Sleeper, saves them and returns all players. When no new list can be fetched, it
    /// returns the players from the database. Null when that fails too.
    /// </summary>
    Task<List<AppPlayer.Player>?> FetchAllPlayersAsync(CancellationToken cancellationToken);

    /// <summary>Returns all players from the database, without fetching. Null when that fails.</summary>
    Task<List<AppPlayer.Player>?> LoadAllPlayersAsync(CancellationToken cancellationToken);
}

public class PlayerLogic : IPlayerLogic
{
    public Task<List<AppPlayer.Player>?> FetchAllPlayersAsync(CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<List<AppPlayer.Player>?> LoadAllPlayersAsync(CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
