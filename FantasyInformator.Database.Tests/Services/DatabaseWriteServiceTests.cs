using FantasyInformator.Database.Context;
using FantasyInformator.Database.Models.Players;
using FantasyInformator.Database.Services;
using FantasyInformator.Domain.Enums;
using FantasyInformator.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyInformator.Database.Tests.Services;

/// <summary>
/// DatabaseWriteService decides by the state of each model what to do with it, so each test saves one Player in one
/// state and then looks at what is in the database. The database is EF's in-memory database, a new one per test.
/// Example: a New Player is added, and a Deleted Player is removed.
/// </summary>
[TestClass]
public sealed class DatabaseWriteServiceTests(TestContext testContext)
{
    private static readonly DateTime MondayUtc = new(2026, 11, 16, 9, 0, 0, DateTimeKind.Utc);

    private ServiceProvider _services = null!;
    private IDbContextFactory<FantasyInformatorDbContext> _contextFactory = null!;
    private DatabaseWriteService _service = null!;

    // Every test gets its own in-memory database, so the tests never see each other's players.
    [TestInitialize]
    public void Initialize()
    {
        var databaseName = Guid.NewGuid().ToString();

        _services = new ServiceCollection()
            .AddDbContextFactory<FantasyInformatorDbContext>(options => options.UseInMemoryDatabase(databaseName))
            .BuildServiceProvider();
        _contextFactory = _services.GetRequiredService<IDbContextFactory<FantasyInformatorDbContext>>();
        _service = new DatabaseWriteService(_contextFactory);
    }

    [TestCleanup]
    public void Cleanup() =>
        _services.Dispose();

    [TestMethod]
    public async Task SaveAll_ShouldAddPlayer_WhenPlayerIsNew()
    {
        var player = CreatePlayer(TrackingState.New, Guid.NewGuid());

        await _service.SaveAll([player], testContext.CancellationToken);

        var saved = await FindPlayerAsync(player.Id);
        Assert.IsNotNull(saved);
        Assert.AreEqual(player.Name, saved.Name);
        Assert.AreEqual(saved.CreatedUtc, saved.UpdatedUtc);
    }

    [TestMethod]
    public async Task SaveAll_ShouldUpdatePlayerAndKeepCreatedUtc_WhenPlayerIsPending()
    {
        var id = Guid.NewGuid();
        await AddPlayerAsync(id, "Old name");
        var player = CreatePlayer(TrackingState.Pending, id);

        await _service.SaveAll([player], testContext.CancellationToken);

        var saved = await FindPlayerAsync(id);
        Assert.IsNotNull(saved);
        Assert.AreEqual(player.Name, saved.Name);
        Assert.AreEqual(MondayUtc, saved.CreatedUtc);
        Assert.IsGreaterThan(MondayUtc, saved.UpdatedUtc);
    }

    [TestMethod]
    public async Task SaveAll_ShouldRemovePlayer_WhenPlayerIsDeleted()
    {
        var id = Guid.NewGuid();
        await AddPlayerAsync(id, "Saquon Barkley");
        var player = CreatePlayer(TrackingState.Deleted, id);

        await _service.SaveAll([player], testContext.CancellationToken);

        Assert.IsNull(await FindPlayerAsync(id));
    }

    [TestMethod]
    public async Task SaveAll_ShouldUpdateDatabaseEntry_WhenNewPlayerHasExternalIdInDatabase()
    {
        var databaseId = Guid.NewGuid();
        await AddPlayerAsync(databaseId, "Old name");
        var player = CreatePlayer(TrackingState.New, Guid.Empty);

        await _service.SaveAll([player], testContext.CancellationToken);

        var saved = await FindPlayerAsync(databaseId);
        Assert.IsNotNull(saved);
        Assert.AreEqual(player.Name, saved.Name);
        Assert.AreEqual(MondayUtc, saved.CreatedUtc);
        Assert.AreEqual(1, await CountPlayersAsync());
    }

    [TestMethod]
    public async Task SaveAll_ShouldSkipPlayer_WhenPlayerIsUnchanged()
    {
        var player = CreatePlayer(TrackingState.Unchanged, Guid.NewGuid());

        await _service.SaveAll([player], testContext.CancellationToken);

        Assert.IsNull(await FindPlayerAsync(player.Id));
    }

    private static Player CreatePlayer(TrackingState state, Guid id) =>
        new()
        {
            Id = id,
            ExternalId = "1234",
            Name = "Saquon Barkley",
            State = state,
        };

    // The player is added straight into the database, created and updated on Monday, so a test can check that an
    // update keeps Monday as its created time.
    private async Task AddPlayerAsync(Guid id, string name)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(testContext.CancellationToken);

        _ = context.Players.Add(new PlayerDb
        {
            Id = id,
            ExternalId = "1234",
            Name = name,
            CreatedUtc = MondayUtc,
            UpdatedUtc = MondayUtc,
        });

        _ = await context.SaveChangesAsync(testContext.CancellationToken);
    }

    private async Task<int> CountPlayersAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync(testContext.CancellationToken);

        return await context.Players.CountAsync(testContext.CancellationToken);
    }

    private async Task<PlayerDb?> FindPlayerAsync(Guid id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(testContext.CancellationToken);

        return await context.Players
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, testContext.CancellationToken);
    }
}
