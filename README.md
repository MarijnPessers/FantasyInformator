# FantasyInformator

A personal application to manage multiple fantasy football teams on [Sleeper](https://sleeper.com). It runs locally on a PC and keeps its data in an Azure SQL database.

## What it can do

- Keep several fantasy teams (Sleeper leagues) side by side, per season.
- Fetch league and roster information from Sleeper only when you ask for it.
- Show each league's roster: which player sits in which slot, and which slots each player could fill.
- Keep what it fetched separate from what is stored. You see what changed and decide what to save.
- Warn you when you leave with unsaved changes.

## Projects

Every functionality is a separate project. A project's README holds its details.

| Project | Purpose |
|---------|---------|
| [FantasyInformator.App](FantasyInformator.App/README.md) | Blazor front end |
| [FantasyInformator.Models](FantasyInformator.Models/README.md) | The application models |
| [FantasyInformator.Sleeper](FantasyInformator.Sleeper/README.md) | Retrieval from Sleeper |
| [FantasyInformator.Nflverse](FantasyInformator.Nflverse/README.md) | Kickoff times from nflverse |
| [FantasyInformator.Database](FantasyInformator.Database/README.md) | Code-first database |
| [FantasyInformator.Migrator](FantasyInformator.Migrator/README.md) | Creates and applies the database migrations |
| [FantasyInformator.State](FantasyInformator.State/README.md) | The state classes, actions and handlers |

Each project except Models has a test project with the same name plus `.Tests`. The models hold no logic, so they have none. The tests use MSTest and AutoFixture.
