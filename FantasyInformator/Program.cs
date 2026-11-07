using FantasyInformator.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBlazor();
builder.LoadConfiguration();

var app = builder.Build();

app.UseHttpPipeline();
app.MapBlazor();

await app.RunAsync();
