using System.Data.Common;
using FantasyInformator.Database.Context;
using FantasyInformator.Database.Services;
using FantasyInformator.Database.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FantasyInformator.Database.Tests.Services;

[TestClass]
public sealed class DatabaseWakeUpServiceTests(TestContext testContext)
{
    private static readonly TimeSpan ShortTimeout = TimeSpan.FromMilliseconds(50);

    [TestMethod]
    public async Task WakeUpAsync_ShouldReturnFalse_WhenTheDatabaseGivesAnError()
    {
        var service = CreateService(_ => throw new TestDbException());

        var result = await service.WakeUpAsync(testContext.CancellationToken);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task WakeUpAsync_ShouldReturnFalse_WhenTheDatabaseDoesNotAnswerInTime()
    {
        var service = CreateService(WaitForever);

        var result = await service.WakeUpAsync(testContext.CancellationToken);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task WakeUpAsync_ShouldThrow_WhenTheCallerCancels()
    {
        var service = CreateService(WaitForever);
        using var callerSource = new CancellationTokenSource();
        await callerSource.CancelAsync();

        _ = await Assert.ThrowsAsync<OperationCanceledException>(() => service.WakeUpAsync(callerSource.Token));
    }

    private static async Task<FantasyInformatorDbContext> WaitForever(CancellationToken cancellationToken)
    {
        await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);

        return null!;
    }

    private static DatabaseWakeUpService CreateService(Func<CancellationToken, Task<FantasyInformatorDbContext>> create) =>
        new(new StubContextFactory(create), Options.Create(new DatabaseSettings { WakeUpTimeout = ShortTimeout }));

    private sealed class StubContextFactory(Func<CancellationToken, Task<FantasyInformatorDbContext>> create)
        : IDbContextFactory<FantasyInformatorDbContext>
    {
        public FantasyInformatorDbContext CreateDbContext() =>
            throw new NotSupportedException();

        public Task<FantasyInformatorDbContext> CreateDbContextAsync(CancellationToken cancellationToken = default) =>
            create(cancellationToken);
    }

    private sealed class TestDbException : DbException;
}
