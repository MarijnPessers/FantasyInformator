using System.Reflection;
using FantasyInformator.Logic.Extensions;
using FantasyInformator.State.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeWarp.State;

namespace FantasyInformator.State.Extensions;

public static class StateServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Binds <see cref="StateSettings"/> and checks it when the application starts, so a missing owner name stops
        /// the start instead of failing when the owners are first needed. Then registers the mediator, TimeWarp.State
        /// and the services of Logic.
        /// </summary>
        public IServiceCollection AddStateServices(IConfiguration configuration)
        {
            _ = services.AddGeneratedMediator<ClientPipeline>();
            _ = services.AddTimeWarpState(x =>
            {
                x.Assemblies = [Assembly.GetExecutingAssembly()];
                _ = x.UseReduxDevTools();
            });

            _ = services.AddOptions<StateSettings>()
                .Bind(configuration.GetSection(StateSettings.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services.AddLogicServices(configuration);
        }
    }
}
