using System.Data.Common;
using FantasyInformator.Database.Context;
using FantasyInformator.Database.Services;
using FantasyInformator.Database.Settings;
using FantasyInformator.Shared.Progress;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FantasyInformator.Database.Tests.Services;

[TestClass]
public sealed class DatabaseWakeUpServiceTests(TestContext testContext)
{
    private static readonly TimeSpan ShortTimeout = TimeSpan.FromMilliseconds(50);

    private RecordingProgress _progress = null!;

    [TestInitialize]
    public void Initialize() =>
        _progress = new RecordingProgress();

    [TestMethod]
    public async Task WakeUpAsync_ShouldReportTheDatabaseIsUnavailable_WhenTheDatabaseGivesAnError()
    {
        var service = CreateService(_ => throw new TestDbException());

        _ = await service.WakeUpAsync(testContext.CancellationToken);

        AssertReportedUnavailable();
    }

    [TestMethod]
    public async Task WakeUpAsync_ShouldReportTheDatabaseIsUnavailable_WhenTheDatabaseDoesNotAnswerInTime()
    {
        var service = CreateService(WaitForever);

        _ = await service.WakeUpAsync(testContext.CancellationToken);

        AssertReportedUnavailable();
    }

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

    private DatabaseWakeUpService CreateService(Func<CancellationToken, Task<FantasyInformatorDbContext>> create) =>
        new(
            new StubContextFactory(create),
            Options.Create(new DatabaseSettings { WakeUpTimeout = ShortTimeout }),
            _progress);

    private void AssertReportedUnavailable()
    {
        var report = Assert.ContainsSingle(_progress.ProgressReports);
        Assert.AreEqual(MessageLevel.Error, report.Severity);
        Assert.IsTrue(report.ShowToUser);
    }

    private sealed class RecordingProgress : IProgress<ProgressReport>
    {
        public List<ProgressReport> ProgressReports { get; } = [];

        public void Report(ProgressReport value) =>
            ProgressReports.Add(value);
    }

    private sealed class StubContextFactory(Func<CancellationToken, Task<FantasyInformatorDbContext>> create)
        : IDbContextFactory<FantasyInformatorDbContext>
    {
        public FantasyInformatorDbContext CreateDbContext() =>
            throw new NotSupportedException();

        public Task<FantasyInformatorDbContext> CreateDbContextAsync(CancellationToken cancellationToken = default) =>
            create(cancellationToken);
    }

    private sealed class TestDbException : DbException
    {
        public TestDbException()
        {
        }

        public TestDbException(string message)
            : base(message)
        {
        }

        public TestDbException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
