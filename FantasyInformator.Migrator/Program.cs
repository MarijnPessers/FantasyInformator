using FantasyInformator.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// The user secrets are added last, so they override the defaults in appsettings.json.
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .AddUserSecrets<Program>()
    .Build();

var databaseOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>() ?? new DatabaseOptions();

// The serverless database pauses itself, so the first connection can fail while it wakes up.
var contextOptions = new DbContextOptionsBuilder<FantasyInformatorDbContext>()
    .UseSqlServer(databaseOptions.ConnectionStringEF, sqlServer => sqlServer.EnableRetryOnFailure())
    .Options;

await using var context = new FantasyInformatorDbContext(contextOptions);

if (!context.Database.GetMigrations().Any())
{
    Console.WriteLine("There are no migrations. Create InitialCreate first, see the Database README.");
    return;
}

var pendingMigrations = (await context.Database.GetPendingMigrationsAsync()).ToList();

if (pendingMigrations.Count == 0)
{
    Console.WriteLine("The database is up to date.");
    return;
}

await context.Database.MigrateAsync();

Console.WriteLine($"Applied {pendingMigrations.Count} migration(s):");

foreach (var migrationName in pendingMigrations)
{
    Console.WriteLine($"  {migrationName}");
}
