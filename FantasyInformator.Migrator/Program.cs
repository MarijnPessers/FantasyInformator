using FantasyInformator.Database.Context;
using FantasyInformator.Database.Settings;
using FantasyInformator.Migrator;
using FantasyInformator.Migrator.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// The user secrets are added last, so they override the defaults in appsettings.json.
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", false)
    .AddUserSecrets<Program>()
    .Build();

var databaseOptions = configuration
    .GetSection(DatabaseSettings.SectionName)
    .Get<DatabaseSettings>() ?? new DatabaseSettings();
var migratorOptions = configuration
    .GetSection(MigratorSettings.SectionName)
    .Get<MigratorSettings>() ?? new MigratorSettings();

// The serverless database pauses itself, so the first connection can fail while it wakes up.
var contextOptions = new DbContextOptionsBuilder<FantasyInformatorDbContext>()
    .UseSqlServer(databaseOptions.ConnectionStringEF, sqlServer => sqlServer.EnableRetryOnFailure())
    .Options;

await using var context = new FantasyInformatorDbContext(contextOptions);

var dotnetEfRunner = new DotnetEfRunner(
    migratorOptions.DatabaseProjectPath,
    migratorOptions.StartupProjectPath,
    databaseOptions.ConnectionStringEF);
var menu = new MigratorMenu(context, dotnetEfRunner);

await menu.RunAsync();
