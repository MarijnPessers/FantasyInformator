using FantasyInformator.Database.Context;
using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Player;
using Microsoft.EntityFrameworkCore;
using AppLeague = FantasyInformator.Models.League;
using AppPlayer = FantasyInformator.Models.Player;

namespace FantasyInformator.Database.Services.League;

public interface ILeagueReadService
{
    /// <summary>Gets all leagues, ordered by name. The leagues come without their league instances.</summary>
    Task<List<AppLeague.League>> GetLeaguesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets the players on the roster of any fantasy team in one league instance (a league in one season).
    /// The players come without their links to other records.
    /// </summary>
    Task<List<AppPlayer.Player>> GetPlayersInLeagueAsync(Guid leagueInstanceId, CancellationToken cancellationToken);
}

public class LeagueReadService(
    IDbContextFactory<FantasyInformatorDbContext> contextFactory)
    : ILeagueReadService
{
    public async Task<List<AppLeague.League>> GetLeaguesAsync(CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var leagues = await context.Leagues
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return [.. leagues.Select(x => x.Map())];
    }

    public async Task<List<AppPlayer.Player>> GetPlayersInLeagueAsync(
        Guid leagueInstanceId,
        CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var players = await context.Players
            .AsNoTracking()
            .Where(p => p.RosteredPlayers.Any(r => r.FantasyTeam!.LeagueInstanceId == leagueInstanceId))
            .ToListAsync(cancellationToken);

        return [.. players.Select(x => x.Map())];
    }
}
