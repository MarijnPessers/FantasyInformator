using FantasyInformator.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FantasyInformator.Database;

public static class DatabaseServiceCollectionExtensions
{
    /// <summary>
    /// Binds <see cref="DatabaseOptions"/> and checks it when the application starts,
    /// so a missing connection string stops the start instead of failing at the first save.
    /// </summary>
    public static IServiceCollection AddDatabaseOptions(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    /// <summary>
    /// Registers the context factory and the save service. A factory is used instead of one shared context,
    /// because a Blazor Server circuit lives long and a context must not.
    /// </summary>
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        _ = services.AddDbContextFactory<FantasyInformatorDbContext>((serviceProvider, builder) =>
            builder.UseSqlServer(
                serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionStringApp));
        _ = services.AddScoped<IDatabaseSaveService, DatabaseSaveService>();

        return services;
    }
}
