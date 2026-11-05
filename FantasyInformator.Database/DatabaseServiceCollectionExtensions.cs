using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyInformator.Database;

public static class DatabaseServiceCollectionExtensions
{
    /// <summary>
    /// Binds <see cref="DatabaseOptions"/> and checks it when the application starts,
    /// so a missing connection string stops the start instead of failing at the first save.
    /// </summary>
    public static IServiceCollection AddDatabaseOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
