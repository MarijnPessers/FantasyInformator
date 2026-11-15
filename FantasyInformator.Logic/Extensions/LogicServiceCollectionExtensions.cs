using FantasyInformator.Database.Extensions;
using FantasyInformator.Sleeper.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyInformator.Logic.Extensions;

public static class LogicServiceCollectionExtensions
{
    /// <summary>
    /// Registers the settings and services of Sleeper, Nflverse and Database, which only Logic uses.
    /// </summary>
    public static IServiceCollection AddLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddDatabaseOptions(configuration);
        _ = services.AddSleeperOptions(configuration);
        _ = services.AddDatabaseServices();

        return services;
    }
}
