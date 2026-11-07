# FantasyInformator

A personal application to manage multiple fantasy football teams on [Sleeper](https://sleeper.com). It runs locally on a PC and keeps its data in an Azure SQL database.

Status: the solution structure is set up. Most functionality is not built yet.

## What it can do

- Keep several fantasy teams (Sleeper leagues) side by side, per season.
- Fetch league and roster information from Sleeper only when you ask for it.
- Show each league's roster: which player sits in which slot, and which slots each player could fill.
- Keep what it fetched separate from what is stored. You see what changed and decide what to save.
- Warn you when you leave with unsaved changes.

## Things to come

Each item gets its own short design session before it is built.

- Drafting, scouting and lineup management.
- Lineup logic with preferred slots.
- Scoring history per player.
- NFL team schedules.
- Wins, losses and matchups.
- Players list and games as global lists you update now and then.
- Claude in the application, to read the stored data and make recommendations.
- Failed saves written to a queue folder and retried in order.
- Finding players to pick up, and comparing team rosters.
- Online access, if the application is ever put online.
- Other data sources when Sleeper lacks something.

The decisions behind this are in [specs/SPEC-1.md](specs/SPEC-1.md).

## Projects

Every functionality is a separate project. A project's README holds its details.

| Project | Purpose |
|---------|---------|
| [FantasyInformator](FantasyInformator/README.md) | Blazor front end |
| [FantasyInformator.Models](FantasyInformator.Models/README.md) | The application models |
| [FantasyInformator.Sleeper](FantasyInformator.Sleeper/README.md) | Retrieval from Sleeper |
| [FantasyInformator.Nflverse](FantasyInformator.Nflverse/README.md) | Kickoff times from nflverse |
| [FantasyInformator.Database](FantasyInformator.Database/README.md) | Code-first database |
| [FantasyInformator.Migrator](FantasyInformator.Migrator/README.md) | Applies the database migrations |

Each project has a test project with the same name plus `Tests`. The tests use MSTest and AutoFixture.
