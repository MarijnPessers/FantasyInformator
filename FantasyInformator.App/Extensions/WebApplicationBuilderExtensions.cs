using FantasyInformator.State.Extensions;

namespace FantasyInformator.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddBlazor(this WebApplicationBuilder builder) =>
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

    public static void AddStateServices(this WebApplicationBuilder builder) =>
        builder.Services.AddStateServices(builder.Configuration);
}
