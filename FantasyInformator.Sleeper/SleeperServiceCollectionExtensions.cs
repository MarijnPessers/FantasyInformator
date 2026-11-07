using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyInformator.Sleeper;

public static class SleeperServiceCollectionExtensions
{
    /// <summary>
    /// Binds <see cref="SleeperOptions"/> and checks it when the application starts,
    /// so a missing username stops the start instead of failing at the first update.
    /// </summary>
    public static IServiceCollection AddSleeperOptions(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddOptions<SleeperOptions>()
            .Bind(configuration.GetSection(SleeperOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
