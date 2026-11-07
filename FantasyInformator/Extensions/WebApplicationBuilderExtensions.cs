using FantasyInformator.Database;
using FantasyInformator.Sleeper;

namespace FantasyInformator.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddBlazor(this WebApplicationBuilder builder) =>
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

    public static void LoadConfiguration(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddDatabaseOptions(builder.Configuration);
        _ = builder.Services.AddSleeperOptions(builder.Configuration);
    }
}
