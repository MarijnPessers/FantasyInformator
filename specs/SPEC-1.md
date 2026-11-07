# SPEC-1: FantasyInformator

Status: decisions from the first grill session, which is finished. The solution structure and NuGet packages are set up (see "Projects"). The application models, the settings classes per project and `appsettings.example.json` are built (see the Models README for the models and the diagram). The Sleeper client, the database, the state model and the screens are not built yet. The Blazor project is still the empty Blazor Server template and is not a starting point for the design.

## Goal

A personal application to manage multiple fantasy football teams (Sleeper leagues): drafting, scouting and lineup management. First milestone: retrieve and show the roster of each league.

- Runs locally on the developer's PC. No security needed. Security is added if it is ever put online.
- Data is stored in an Azure SQL database (relational, free tier). The database is the source of truth.

## Terminology

- **Update**: fetch data from Sleeper into the application state. Never writes to the database.
- **Save**: write the state changes to the database.
- **Season**: a year. A renewed Sleeper league is a new league with a new id, so a season is its own record.
- **League** and **league instance**: a league is the fantasy league as you know it and only has a name. A league instance is that league in one season. It belongs to a season and holds the Sleeper league id.

## Rules for the whole application

- Nothing is read from Sleeper or written to the database unless a button is pressed.
- The only fetch that is saved to the database immediately, without a Save button, is the global players list. Sleeper allows it once a day, so a forgotten save would cost the list for 24 hours.
- When the overview opens, the stored seasons, leagues and rosters are loaded from the database without a button. Reading the own database is not a Sleeper call. Only Sleeper calls and saves need a button.
- Before the first Sleeper call of a session, call `state/nfl`. The current season and week are kept in state as variables and are never saved.
- Always use UTC.
- Buttons use standard names and a matching color scheme: primary, secondary, warning, error and so on.
- Update and report buttons use SVG icons.
- The user is warned when leaving the site with unsaved changes.
- Unofficial Sleeper calls can be blocked. If a call fails, the rest of the application must keep working.
- Sleeper is the preferred source for all data. If the information is available somewhere else in Sleeper, use that.

## Projects

Every functionality is a separate project. Each project has a sibling test project with the same folder structure. Test classes are named `<ClassName>Tests.cs`. Tests use MSTest and AutoFixture.

| Project | Purpose |
|---------|---------|
| `FantasyInformator` | Blazor app, front end only |
| `FantasyInformator.Models` | The application models |
| `FantasyInformator.Sleeper` | Retrieval from Sleeper, with its own Sleeper models and mappers to the application models |
| `FantasyInformator.Database` | Code-first database, its own database models, mappers to the application models, and the services to call |
| `FantasyInformator.SleeperTests`, `FantasyInformator.DatabaseTests`, `FantasyInformatorTests` | Test projects |

- Mappers live in the project that maps to the application models, and are written by hand. No AutoMapper. Every mapper has tests.
- More projects may follow. Candidates: a State project for the state classes and handlers, and a project for slot logic. Decide when the code asks for it.
- Whether TimeWarp.State actions and handlers can live in a project other than the Blazor app is not verified. Check before building.

## Blazor

- State management: **TimeWarp.State** (previously Blazor-State). Latest stable 11.0.3. It supports .NET 8 and is compatible with .NET 10.
- Every page has a code-behind. The template holds no logic except loops and `if` on booleans.

## Change tracking

- Every application model has a tracking state (`TrackingState`: New, Pending, Unchanged, Deleted) and an updated time (`TrackedModel`). The exception is `LastSleeperCall`, which has neither.
- The pending value is not part of the application models or the database models. It belongs to the state model, which is built when the app state is worked on. That model holds, per item, the current value, the pending value, the tracking state and the updated time. Bring this back into focus when starting on the state (see "Open").
- Each item holds a current value and, only when it differs, a pending value. The current value is always Unchanged. The pending value is never equal to the current value.
- A New item has only a pending value and stays New through further updates until it is saved.
- A Deleted item has a pending value identical to the current one, with its state set to Deleted.
- A second update overwrites the pending value. A pending value equal to the current one is dropped.
- Items with a pending value get a slightly different color. A report icon button on a league or a player compares current with pending.
- After a save, the pending value becomes the current value and is cleared.
- Save writes only New, Pending and Deleted objects, one statement per changed object. A parent update does not update unchanged children. Unchanged objects are never sent to the database. Absence from a save never deletes anything. Only an object marked Deleted is deleted, and a delete cascades. A New complex object inserts records in all its tables.
- Every object has an updated time, set when that object itself changes. An update that finds no differences changes nothing, and Save stays disabled.
- Exception: the global lists (the players list, and later games) always get a new update time on an update. The players list is saved at once ("Rules for the whole application").

## Overview screen

- Tabs per season: the current year first, then earlier years in descending order.
- Each tab shows the leagues of that season as styled divs. No grid or table.
- Each league shows how long ago it was updated, an Update button for its contents (players), a Save button (disabled when nothing changed) and a report icon.
- Each tab has an Update button for the leagues of that tab's year. It calls only the user-leagues endpoint, with no players, scores or records.
- Next to it, a checkbox "attempt to get next season": take the last known year plus one and try to get leagues. If there are some, create a new season record and show a new tab for it.
- A league that already exists (matched by id) is only changed when its name differs.
- The report icon on a league or a player opens a modal with a Close button. It shows a summary of the changes. Each line is an accordion, and clicking it shows the full details: each changed field as the current value next to the pending value. A New item shows as added, a Deleted item as removed. A league's report also covers its changed players.

## Slots and positions

Slots and positions are tables, not enums. They are built as models (see the Models README).

- `Slot`: the Sleeper code, a name and an abbreviation. `LeagueSlot` links a league instance to a slot and holds how many of that slot the league has. Only slots that exist in the league are linked. `RosteredPlayer` holds the slot a player is in right now.
- `Position`: a name and an abbreviation. A player has one or more positions (`PlayerPosition`).
- `SlotEligibility` links a position to the slots it can start in (one or more) and holds `IsPreferred`: the position is preferred to start in that slot.
- When a player arrives from Sleeper with a position that is not known, the position is added. The front end shows a constant warning while a position has no slot link.
- Starting data (to be seeded when the database is created): the slots QB, RB, WR, TE, Flex, SuperFlex, K, DL, LB, IDP (Sleeper's `IDP_FLEX`), DB, BN, IR and TAX. The eligible slots by position: QB: QB, SuperFlex. RB, WR, TE: own slot, Flex, SuperFlex. K: K. DL, DE: DL, IDP. LB: LB, IDP. DB, CB, S: DB, IDP. Everyone: BN.
- IR and TAX eligibility depend on injury status and rookie years and come later. The slot Sleeper reports is the truth, even when it is not a preferred slot, for example a DB in IDP.

## First milestone data

- Roster: player names, eligible slots, the slot each player is in now, and how many slots of each kind the league has.
- Slot counts come from the league call: `roster_positions`, and `settings.taxi_slots` and `settings.reserve_slots`.
- A league's players update calls the rosters endpoint once. That call returns the rosters of all teams in the league, as player ids with the starters, IR and taxi lists. It returns no names or player details. All rosters are kept, not only the developer's own.
- Owners are a separate update (the league-users endpoint). A roster holds its owner id as a plain value.
- Player ids on a roster and owner ids on a roster are plain values with no database foreign key. They may point to a player or owner that is not known, for example an owner who left the league.
- The players list (about 5 MB, at most once a day per the Sleeper docs) is a separate table in a global update section. It is fetched by hand, only when needed: once at the start, and again when rookies are added. The Update button is locked for 24 hours after a fetch. The time of the last fetch is kept in `LastSleeperCall`, one row per call name. The list is saved to the database immediately on fetch. Names and positions come from this list.

## Sleeper API facts (verified against docs.sleeper.com and live calls)

- Read-only and public. No token or credentials are needed.
- User: `GET https://api.sleeper.app/v1/user/<username>`
- Leagues: `GET https://api.sleeper.app/v1/user/<user_id>/leagues/nfl/<season>`
- League: `GET https://api.sleeper.app/v1/league/<league_id>`
- Rosters: `GET https://api.sleeper.app/v1/league/<league_id>/rosters`
- Players: `GET https://api.sleeper.app/v1/players/nfl`
- State: `GET https://api.sleeper.app/v1/state/nfl`
- Rate guideline: under 1000 calls a minute.
- Unofficial and undocumented, so they can change or be blocked:
  - `GET https://api.sleeper.com/schedule/nfl/regular/<season>`
  - `GET https://api.sleeper.com/stats/nfl/<season>/<week>?season_type=regular&position[]=<position>`

## Azure SQL setup (in progress, done by the developer)

- Azure SQL Database free tier (General Purpose, serverless). Behavior at the free limit: auto-pause until next month.
- Created through https://aka.ms/azuresqlhub with Start free. The server may be in another region than the resource group.
- No Networking tab in that flow. Add the PC's IP address to the server firewall after creation: SQL server, Networking, Selected networks, Add your client IPv4 address, Save.
- The connection strings go in .NET user secrets, outside the solution folder. Never in a file in the solution.
- Three database users are planned: `ef` (create and drop tables, read and write, used for migrations), `app` (read and write, used while the application runs) and `claude` (read only).
- The database is created and migrated only when the developer asks for it.

## Configuration

The application can be used by anyone. Everything specific to a person or a PC is set in .NET user secrets, which override `appsettings.json`:

- `Sleeper:Username`: the Sleeper username.
- `Database:ConnectionStringApp`: the connection string of the `app` user. Required.
- `Database:ConnectionStringEF`: the connection string of the `ef` user, used for migrations.
- `Database:FailedSaveQueueFolder`: the folder of the failed-save queue.

Each project has its own settings class (`DatabaseOptions`, `SleeperOptions`) and checks it when the application starts. `appsettings.json` holds the default values for everything else. `appsettings.example.json` shows every key. Someone who puts their own values in `appsettings.json` has chosen that themselves. No path is written in code.

.NET loads user secrets by default only in the Development environment. Not verified for .NET 10.

## Open

- Whether the State project and a slot-logic project are needed.
- The state model with the pending values (see "Change tracking"). Build it first when starting on the app state, and decide where it lives: the Blazor project or a State project.

## For the future

Each item gets its own mini-grill when it is started.

- Drafting, scouting and lineup management.
- Fantasy dynamics: player status, taxi squad, IR rules (`reserve_allow_*`, `taxi_years`, `taxi_allow_vets`).
- Lineup logic with preferred slots (for example IDP filled with LBs first, other players only when there are not enough LBs).
- Per-game scoring history for players. The current year is read by default, and a full-history command is separate. Only missing weeks are read. A per-week reload button exists for score corrections. An unchanged result stays unchanged.
- NFL team schedules on their own screen, used to know which weeks a player played. A traded player needs the team he played for each week.
- Wins and losses and matchups (a matchup is between two rosters in the same league). Records are defined when matchups are fetched.
- Claude in the application: reads the stored data, gathers its own data and makes recommendations (Anthropic API, separate billing).
- Failed saves: when any save fails, write what was to be saved as a JSON file in a queue folder. Name the files by UTC timestamp down to microseconds so the names are unique. A retry button processes the queued files in chronological order. The folder comes from configuration in user secrets. The database is not expected to be offline much.
- Finding players to pick up: AI searches the internet for available players, and the stored rosters of all teams in a league show which of them are already taken.
- Comparing team rosters, for example to find which team is heavy in a position when looking for a trade.
- Putting the application online, with security.
- Other data sources if Sleeper lacks something.
