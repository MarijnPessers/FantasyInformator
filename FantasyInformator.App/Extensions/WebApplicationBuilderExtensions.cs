using FantasyInformator.App.Settings;
using FantasyInformator.State.Extensions;

namespace FantasyInformator.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void AddBlazor() =>
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

        /// <summary>Binds <see cref="AppSettings"/> from the "App" section.</summary>
        public void AddAppOptions() =>
            _ = builder.Services.AddOptions<AppSettings>()
                .Bind(builder.Configuration.GetSection(AppSettings.SectionName));

        public void AddStateServices() =>
            builder.Services.AddStateServices(builder.Configuration);
    }
}
