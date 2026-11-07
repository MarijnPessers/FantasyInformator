using FantasyInformator.Components;

namespace FantasyInformator.Extensions;

public static class WebApplicationExtensions
{
    public static void UseHttpPipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            _ = app.UseExceptionHandler("/Error", true);

            // The default HSTS value is 30 days. It may need to change for production,
            // see https://aka.ms/aspnetcore-hsts.
            _ = app.UseHsts();
        }

        _ = app.UseStatusCodePagesWithReExecute("/not-found", null, true);
        _ = app.UseHttpsRedirection();
        _ = app.UseAntiforgery();
    }

    public static void MapBlazor(this WebApplication app)
    {
        _ = app.MapStaticAssets();
        _ = app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }
}
