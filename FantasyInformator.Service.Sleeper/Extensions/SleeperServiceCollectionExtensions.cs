using FantasyInformator.Service.Sleeper.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyInformator.Service.Sleeper.Extensions;

public static class SleeperServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Binds <see cref="SleeperSettings"/> and checks it when the application starts,
        /// so a missing username stops the start instead of failing at the first update.
        /// </summary>
        public IServiceCollection AddSleeperOptions(IConfiguration configuration)
        {
            _ = services.AddOptions<SleeperSettings>()
                .Bind(configuration.GetSection(SleeperSettings.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
    }
}
