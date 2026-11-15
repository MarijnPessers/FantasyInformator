using FantasyInformator.App.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBlazor();
builder.AddAppServices();

var app = builder.Build();

app.UseHttpPipeline();
app.MapBlazor();

await app.RunAsync();
