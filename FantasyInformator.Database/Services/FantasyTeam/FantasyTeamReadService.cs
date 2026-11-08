using FantasyInformator.Database.Context;
using FantasyInformator.Database.Mappers.Player;
using Microsoft.EntityFrameworkCore;
using AppPlayer = FantasyInformator.Models.Player;

namespace FantasyInformator.Database.Services.FantasyTeam;

public interface IFantasyTeamReadService
{
    /// <summary>
    /// Gets the players on the roster of one fantasy team. The players come without their links to other records.
    /// </summary>
    Task<List<AppPlayer.Player>> GetPlayersOnFantasyTeamAsync(Guid fantasyTeamId, CancellationToken cancellationToken);
}

public class FantasyTeamReadService(
    IDbContextFactory<FantasyInformatorDbContext> contextFactory)
    : IFantasyTeamReadService
{
    public async Task<List<AppPlayer.Player>> GetPlayersOnFantasyTeamAsync(
        Guid fantasyTeamId,
        CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var players = await context.Players
            .AsNoTracking()
            .Where(p => p.RosteredPlayers.Any(r => r.FantasyTeamId == fantasyTeamId))
            .ToListAsync(cancellationToken);

        return [.. players.Select(x => x.Map())];
    }
}
