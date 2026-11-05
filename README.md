# FantasyInformator

A personal application to manage multiple fantasy football teams on [Sleeper](https://sleeper.com). It runs locally on a PC and keeps its data in an Azure SQL database.

Status: the solution structure is set up. The functionality below is planned and not built yet.

## What it can do

- Keep several fantasy teams (Sleeper leagues) side by side, per season.
- Fetch league and roster information from Sleeper only when you ask for it.
- Show for each league the roster: which player sits in which slot, which slots each player could fill, and how many slots of each kind the league has.
- Keep what it fetched separate from what is stored. You see what changed and decide what to save. The database stays the source of truth.
- Warn you when you leave with unsaved changes.

## Things to come

Each item gets its own short design session before it is built.

- **Drafting, scouting and lineup management**, including fantasy dynamics such as player status, the taxi squad and injured reserve rules.
- **Lineup logic with preferred slots.** Example: individual defensive player (IDP) slots are filled with linebackers first, and other players only when there are not enough.
- **Scoring history per player.** The current year is read by default, a full history is a separate command, and only missing weeks are read. Each week can be reloaded to pick up score corrections.
- **NFL team schedules** on their own screen, so the app knows in which weeks a player played, also after a trade.
- **Wins, losses and matchups** between teams in a league.
- **Players list and games** as global lists that you update now and then. The players list can be updated once every 24 hours.
- **Claude in the application:** reading the stored data, gathering its own data and making recommendations.
- **Failed saves** are written to a queue folder as JSON and can be retried in order.
- **Finding players to pick up** with AI, and **comparing team rosters**, for example to find a trade partner.
- **Online access** with security, if the application is ever put online.
- **Other data sources** when Sleeper lacks something. Sleeper stays the preferred source, and a failing unofficial Sleeper call must never stop the rest of the application.

The decisions behind this are in [specs/SPEC-1.md](specs/SPEC-1.md).

## Projects

Every functionality is a separate project. A project's README holds its technical details.

| Project | Purpose |
|---------|---------|
| [FantasyInformator](FantasyInformator/README.md) | Blazor front end |
| [FantasyInformator.Models](FantasyInformator.Models/README.md) | The application models |
| [FantasyInformator.Sleeper](FantasyInformator.Sleeper/README.md) | Retrieval from Sleeper |
| [FantasyInformator.Database](FantasyInformator.Database/README.md) | Code-first database and the services to use it |

Each project has a test project with the same name plus `Tests`. The tests use MSTest and AutoFixture.
