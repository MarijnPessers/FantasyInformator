using FantasyInformator.Logic.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyInformator.State.Extensions;

public static class StateServiceCollectionExtensions
{
    public static IServiceCollection AddStateServices(this IServiceCollection services, IConfiguration configuration) =>
        services.AddLogicServices(configuration);
}
