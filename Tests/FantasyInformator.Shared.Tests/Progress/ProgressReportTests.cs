using System.Diagnostics;
using AutoFixture;
using FantasyInformator.Shared.Progress;

namespace FantasyInformator.Shared.Tests.Progress;

[TestClass]
public class ProgressReportTests
{
    private Fixture _fixture = null!;

    [TestInitialize]
    public void Initialize() =>
        _fixture = new Fixture();

    [TestMethod]
    public void CreatedUtc_ShouldBeTheTimeTheReportWasMade_WhenNotSet()
    {
        var before = DateTime.UtcNow;

        var report = CreateReport();

        var after = DateTime.UtcNow;
        Assert.IsTrue(report.CreatedUtc >= before && report.CreatedUtc <= after);
        Assert.AreEqual(DateTimeKind.Utc, report.CreatedUtc.Kind);
    }

    [TestMethod]
    public void TraceId_ShouldBeTheTraceIdOfTheRunningActivity_WhenAnActivityRuns()
    {
        using var activity = new Activity(_fixture.Create<string>()).Start();

        var report = CreateReport();

        Assert.AreEqual(activity.TraceId, report.TraceId);
    }

    [TestMethod]
    public void TraceId_ShouldBeNull_WhenNoActivityRuns()
    {
        Activity.Current = null;

        var report = CreateReport();

        Assert.IsNull(report.TraceId);
    }

    [TestMethod]
    public void ActivityName_ShouldBeTheNameOfTheRunningActivity_WhenAnActivityRuns()
    {
        using var activity = new Activity(_fixture.Create<string>()).Start();

        var report = CreateReport();

        Assert.AreEqual(activity.OperationName, report.ActivityName);
    }

    [TestMethod]
    public void ActivityName_ShouldBeNull_WhenNoActivityRuns()
    {
        Activity.Current = null;

        var report = CreateReport();

        Assert.IsNull(report.ActivityName);
    }

    private ProgressReport CreateReport() =>
        new(
            _fixture.Create<MessageLevel>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<bool>(),
            new InvalidOperationException());
}
