using FantasyInformator.Database.Services;
using FantasyInformator.Logic.Services.Databases;
using NSubstitute;

namespace FantasyInformator.Logic.Tests.Services.Databases;

[TestClass]
public sealed class DatabaseWakeUpLogicTests(TestContext testContext)
{
    private IDatabaseWakeUpService _databaseWakeUpService = null!;
    private DatabaseWakeUpLogic _logic = null!;

    [TestInitialize]
    public void Initialize()
    {
        _databaseWakeUpService = Substitute.For<IDatabaseWakeUpService>();
        _logic = new DatabaseWakeUpLogic(_databaseWakeUpService);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public async Task WakeUpAsync_ShouldReturnWhatTheServiceReturns_WhenCalled(bool isAwake)
    {
        _ = _databaseWakeUpService.WakeUpAsync(testContext.CancellationToken).Returns(isAwake);

        var result = await _logic.WakeUpAsync(testContext.CancellationToken);

        Assert.AreEqual(isAwake, result);
    }
}
