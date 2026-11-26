using System.Data.Common;
using FantasyInformator.Database.Context;
using FantasyInformator.Database.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FantasyInformator.Database.Services;

/// <summary>Wakes the database, which pauses itself when it isn't used.</summary>
public interface IDatabaseWakeUpService
{
    /// <summary>
    /// Runs <c>SELECT 1</c> once, without retry. False when the database gives an error or doesn't answer within
    /// <see cref="DatabaseSettings.WakeUpTimeout"/>.
    /// </summary>
    Task<bool> WakeUpAsync(CancellationToken cancellationToken);
}

internal class DatabaseWakeUpService(
    IDbContextFactory<FantasyInformatorDbContext> contextFactory,
    IOptions<DatabaseSettings> settings) : IDatabaseWakeUpService
{
    public async Task<bool> WakeUpAsync(CancellationToken cancellationToken)
    {
        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutSource.CancelAfter(settings.Value.WakeUpTimeout);

        try
        {
            await using var context = await contextFactory.CreateDbContextAsync(timeoutSource.Token);
            _ = await context.Database.ExecuteSqlRawAsync("SELECT 1", timeoutSource.Token);

            return true;
        }
        catch (DbException)
        {
            return false;
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return false;
        }
    }
}
