using FantasyInformator.State.Extensions;

namespace FantasyInformator.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void AddBlazor() =>
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

        public void AddStateServices() =>
            builder.Services.AddStateServices(builder.Configuration);
    }
}
