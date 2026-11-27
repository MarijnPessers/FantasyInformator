namespace FantasyInformator.App.Extensions;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public void UseHttpPipeline()
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

        public void MapBlazor()
        {
            _ = app.MapStaticAssets();
            _ = app.MapRazorComponents<Components.App>()
                .AddInteractiveServerRenderMode();
        }
    }
}
