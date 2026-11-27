using System.Data.Common;
using FantasyInformator.Database.Context;
using FantasyInformator.Database.Resources;
using FantasyInformator.Database.Settings;
using FantasyInformator.Shared.Progress;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FantasyInformator.Database.Services;

/// <summary>Wakes the database, which pauses itself when it isn't used.</summary>
public interface IDatabaseWakeUpService
{
    /// <summary>
    /// Runs <c>SELECT 1</c> once, without retry. False when the database gives an error or doesn't answer within
    /// <see cref="DatabaseSettings.WakeUpTimeout"/>; it then reports that the database isn't available.
    /// </summary>
    Task<bool> WakeUpAsync(CancellationToken cancellationToken);
}

internal class DatabaseWakeUpService(
    IDbContextFactory<FantasyInformatorDbContext> contextFactory,
    IOptions<DatabaseSettings> settings,
    IProgress<ProgressReport> progress) : IDatabaseWakeUpService
{
    private static readonly TimeSpan ConnectRetryInterval = TimeSpan.FromSeconds(10);

    public async Task<bool> WakeUpAsync(CancellationToken cancellationToken)
    {
        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutSource.CancelAfter(settings.Value.WakeUpTimeout);

        try
        {
            await using var context = await contextFactory.CreateDbContextAsync(timeoutSource.Token);
            context.Database.SetConnectionString(CreateWakeUpConnectionString());
            context.Database.SetCommandTimeout(settings.Value.WakeUpTimeout);
            _ = await context.Database.ExecuteSqlRawAsync("SELECT 1", timeoutSource.Token);

            return true;
        }
        catch (DbException exception)
        {
            ReportUnavailable(exception);

            return false;
        }
        catch (OperationCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            ReportUnavailable(exception);

            return false;
        }
    }

    private void ReportUnavailable(Exception exception) =>
        progress.Report(new ProgressReport(
            MessageLevel.Error,
            Texts.DatabaseUnavailableHeader,
            Texts.DatabaseUnavailableMessage,
            ShowToUser: true,
            exception));

    private string CreateWakeUpConnectionString() =>
        new SqlConnectionStringBuilder(settings.Value.ConnectionStringApp)
        {
            ConnectTimeout = (int)settings.Value.WakeUpTimeout.TotalSeconds,
            ConnectRetryInterval = (int)ConnectRetryInterval.TotalSeconds,
            ConnectRetryCount = Math.Clamp((int)(settings.Value.WakeUpTimeout / ConnectRetryInterval), 0, 255),
        }.ConnectionString;
}
