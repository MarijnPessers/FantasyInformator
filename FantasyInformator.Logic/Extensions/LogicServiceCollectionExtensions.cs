using FantasyInformator.Database.Extensions;
using FantasyInformator.Logic.Services.Databases;
using FantasyInformator.Logic.Services.FantasyTeams;
using FantasyInformator.Logic.Services.Players;
using FantasyInformator.Service.Sleeper.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyInformator.Logic.Extensions;

public static class LogicServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers Logic's own services, and the settings and services of Sleeper, Nflverse and Database, which only
        /// Logic uses.
        /// </summary>
        public IServiceCollection AddLogicServices(IConfiguration configuration)
        {
            _ = services.AddDatabaseOptions(configuration);
            _ = services.AddSleeperOptions(configuration);
            _ = services.AddDatabaseServices();
            _ = services.AddScoped<IDatabaseWakeUpLogic, DatabaseWakeUpLogic>();
            _ = services.AddScoped<IOwnerLogic, OwnerLogic>();
            _ = services.AddScoped<IPlayerLogic, PlayerLogic>();

            return services;
        }
    }
}
