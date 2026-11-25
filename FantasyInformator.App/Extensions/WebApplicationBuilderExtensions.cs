using FantasyInformator.App.Settings;
using FantasyInformator.State.Extensions;

namespace FantasyInformator.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddBlazor(this WebApplicationBuilder builder) =>
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddOptions<AppSettings>()
            .Bind(builder.Configuration.GetSection(AppSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        _ = builder.Services.AddStateServices(builder.Configuration);
    }
}
