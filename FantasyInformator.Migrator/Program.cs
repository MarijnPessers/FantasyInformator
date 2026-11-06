using FantasyInformator.Database;
using FantasyInformator.Migrator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// The user secrets are added last, so they override the defaults in appsettings.json.
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .AddUserSecrets<Program>()
    .Build();

var databaseOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>() ?? new DatabaseOptions();
var migratorOptions = configuration.GetSection(MigratorOptions.SectionName).Get<MigratorOptions>() ?? new MigratorOptions();

// The serverless database pauses itself, so the first connection can fail while it wakes up.
var contextOptions = new DbContextOptionsBuilder<FantasyInformatorDbContext>()
    .UseSqlServer(databaseOptions.ConnectionStringEF, sqlServer => sqlServer.EnableRetryOnFailure())
    .Options;

await using var context = new FantasyInformatorDbContext(contextOptions);

var menu = new MigratorMenu(context, new DotnetEfRunner(migratorOptions.DatabaseProjectPath, databaseOptions.ConnectionStringEF));

await menu.RunAsync();
