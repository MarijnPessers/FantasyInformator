using FantasyInformator.Database.Context;
using FantasyInformator.Database.Services;
using FantasyInformator.Database.Services.FantasyTeam;
using FantasyInformator.Database.Services.League;
using FantasyInformator.Database.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FantasyInformator.Database.Extensions;

public static class DatabaseServiceCollectionExtensions
{
    /// <summary>
    /// Binds <see cref="DatabaseSettings"/> and checks it when the application starts,
    /// so a missing connection string stops the start instead of failing at the first save.
    /// </summary>
    public static IServiceCollection AddDatabaseOptions(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddOptions<DatabaseSettings>()
            .Bind(configuration.GetSection(DatabaseSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    /// <summary>
    /// Registers the context factory, the save service and the read services. A factory is used instead of one
    /// shared context, because a Blazor Server circuit lives long and a context must not.
    /// </summary>
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        _ = services.AddDbContextFactory<FantasyInformatorDbContext>((serviceProvider, builder) =>
            builder.UseSqlServer(
                serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value.ConnectionStringApp));
        _ = services.AddScoped<IDatabaseSaveService, DatabaseSaveService>();
        _ = services.AddScoped<ILeagueReadService, LeagueReadService>();
        _ = services.AddScoped<IFantasyTeamReadService, FantasyTeamReadService>();

        return services;
    }
}
