# Delete leftover obj folders after closing Visual Studio

- After closing Visual Studio, delete every folder at an old project path that holds only `obj`:
  - In the root: `FantasyInformator.App.Tests`, `FantasyInformator.Database.Tests`, `FantasyInformator.Logic.Tests`, `FantasyInformator.Migrator.Tests`, `FantasyInformator.Nflverse`, `FantasyInformator.Service.Nflverse.Tests`, `FantasyInformator.Service.Sleeper.Tests`, `FantasyInformator.Shared.MapGenerator`, `FantasyInformator.Shared.MapGenerator.Tests`, `FantasyInformator.Shared.Tests`, `FantasyInformator.Sleeper`, `FantasyInformator.State.Tests`, `FantasyInformator.Tests`
  - In `Tests/`: `FantasyInformator.Nflverse.Tests`, `FantasyInformator.Sleeper.Tests`
