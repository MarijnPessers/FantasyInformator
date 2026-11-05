using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FantasyInformator.Database;

/// <summary>
/// Used only by the EF command line tools (<c>dotnet ef</c>). It builds the context with the connection string of
/// the <c>ef</c> user, which is allowed to create and change tables, from the .NET user secrets.
/// </summary>
public class FantasyInformatorDbContextFactory : IDesignTimeDbContextFactory<FantasyInformatorDbContext>
{
    public FantasyInformatorDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<FantasyInformatorDbContextFactory>()
            .Build();

        var databaseOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>() ?? new DatabaseOptions();

        var contextOptions = new DbContextOptionsBuilder<FantasyInformatorDbContext>()
            .UseSqlServer(databaseOptions.ConnectionStringEF)
            .Options;

        return new FantasyInformatorDbContext(contextOptions);
    }
}
