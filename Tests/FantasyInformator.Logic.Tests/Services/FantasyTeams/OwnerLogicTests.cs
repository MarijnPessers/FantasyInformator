using AutoFixture;
using FantasyInformator.Database.Services.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;
using FantasyInformator.Logic.Services.FantasyTeams;
using NSubstitute;

namespace FantasyInformator.Logic.Tests.Services.FantasyTeams;

[TestClass]
public sealed class OwnerLogicTests(TestContext testContext)
{
    private const int OwnerCount = 3;

    private Fixture _fixture = null!;
    private IOwnerService _ownerService = null!;
    private OwnerLogic _logic = null!;

    [TestInitialize]
    public void Initialize()
    {
        _fixture = new Fixture();
        _ownerService = Substitute.For<IOwnerService>();
        _logic = new OwnerLogic(_ownerService);
    }

    [TestMethod]
    public async Task LoadAllOwnersAsync_ShouldReturnAllOwners_WhenDatabaseHasOwners()
    {
        var owners = _fixture.Build<Owner>().Without(x => x.FantasyTeams).CreateMany(OwnerCount).ToList();
        _ = _ownerService.GetAll(testContext.CancellationToken).Returns(owners);

        var result = await _logic.LoadAllOwnersAsync(testContext.CancellationToken);

        Assert.IsNotNull(result);
        Assert.AreSequenceEqual(owners, result);
    }
}
