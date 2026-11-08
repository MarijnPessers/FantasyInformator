# SPEC-1: FantasyInformator

Status (8 Nov 2026): decisions from two grill sessions, both finished. The solution structure, the application models, the settings classes, the mappers, the database project (models, mappers, EF context, seed data, three migrations), the service interfaces of Sleeper and Nflverse, and a State project with a proof are in place. The HTTP clients, the database read and save services in use, the state model and the screens are not built yet. The Blazor project is still the empty Blazor Server template and is not a starting point for the design.

Nothing from the second session has been built or run: the new State projects, the interface changes, `Season.CurrentWeek` and the nullable `OwnerId`. The developer builds and runs; Claude never does. The developer ran the State proof test once, on TimeWarp.State 11.0.3, and it passed.

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
- Before the first Sleeper call of a session, call `state/nfl`. The current season and week are kept in state as variables and are never saved (`Season.CurrentWeek`, see "Change tracking").
- Always use UTC.
- Buttons use standard names and a matching color scheme: primary, secondary, warning, error and so on.
- Update and report buttons use SVG icons.
- The user is warned when leaving the site with unsaved changes.
- Unofficial Sleeper calls can be blocked. If a call fails, the rest of the application must keep working.
- Sleeper is the preferred source for all data. If the information is available somewhere else in Sleeper, use that.
- The app never gets information about old leagues from Sleeper. Earlier seasons on the overview are only those already stored.

## Projects

Every functionality is a separate project. Each project has a sibling test project with the same folder structure. Test classes are named `<ClassName>Tests.cs`. Tests use MSTest and AutoFixture.

| Project | Purpose |
|---------|---------|
| `FantasyInformator.App` | Blazor app, front end only |
| `FantasyInformator.Models` | The application models |
| `FantasyInformator.Sleeper` | Retrieval from Sleeper, with its own Sleeper models (private to the project) and mappers to the application models |
| `FantasyInformator.Nflverse` | Kickoff times from the public nflverse schedule, with its own models and a mapper |
| `FantasyInformator.Database` | Code-first database, its own database models, mappers to the application models, the EF context with seed data, and the services to call |
| `FantasyInformator.Migrator` | Console menu that creates and applies the database migrations |
| `FantasyInformator.State` | The state classes, actions and handlers (new, holds a proof) |
| `...Sleeper.Tests`, `...Nflverse.Tests`, `...Database.Tests`, `...State.Tests`, `...App.Tests` | Test projects |

- Mappers live in the project that maps to the application models, and are written by hand. Every mapper has tests.
- **A service takes and returns only application models.** The models of an outside source stay inside the project that reads it, and the service maps them before it returns. The interfaces of Sleeper (except two methods, see "Open") and Nflverse already do this. How many Sleeper calls sit behind one service method is decided when that service is built: a request asks for data X, the service gathers it, maps it and returns application models.
- **Who knows what:** the handler (State project) knows where to get the data. It asks a service, the service returns application models, and the handler writes them into the state. The Blazor app only sends requests (actions) and shows the state. So `FantasyInformator.App` references only `FantasyInformator.State` and `FantasyInformator.Models`. `FantasyInformator.State` references Sleeper, Nflverse, Database and Models, and registers their services and settings behind one extension method. The app's configuration files (`appsettings.json`, user secrets) stay in the app, and the app hands its configuration to that method. Mappers stay pure and static, and the source projects do not depend on the State project. Today the App still references Database and Sleeper (and `Microsoft.EntityFrameworkCore.Design`); that changes in "Next steps". The app stays the startup project of the EF tools (it reaches the Database project through State); check this with the next migration.
- A project for slot logic is a candidate. Decide when the code asks for it.

## Working with the developer

Learned in the second session. The rule files in `.claude/rules/` say much of this in formal terms; the rule files hold only general rules, and everything specific to this project goes in a spec.

- Plain words, short answers, a TL;DR when asked. When the developer says "I don't understand", re-explain with a concrete example instead of adding detail.
- One question at a time. Every option in a question is a real choice; never add a "No" or an "outdated" option as a strawman.
- Nothing is changed before the word "Implement". Claude never builds, tests or runs. The developer builds and pastes the output, and may say "run everything yourself", which overrides the rule for that command only.
- A decision is only real when the developer has answered. Do not decide for the developer.
- The current branch is `main` and has no story number, so the developer names spec files.

## Blazor and state management

- Every page has a code-behind. The template holds no logic except loops and `if` on booleans.
- **TimeWarp.State, version 12, always.** MediatR is not allowed anywhere (see the rule "Banned packages"), and TimeWarp.State 11.x depends on it. Version 12 uses `TimeWarp.Mediator` instead (a separate package, MediatR-shaped). The newest is 12.0.0-beta.17 (a prerelease from 6 Nov 2026); there is no stable 12 yet. **At the start of a session, once in a while, ask the developer to check whether TimeWarp.State 12 is out of beta** (the NuGet page of TimeWarp.State).
- Version 12 targets `net11.0` only. So every project moves from `net10.0` to `net11.0`, and the developer needs the .NET 11 SDK. The handler base type is `StateActionHandler<T>` instead of `ActionHandler<T>`. The Blazor parts are a separate package, `TimeWarp.State.Blazor`, and Blazor hosts call `AddTimeWarpStateBlazor()` besides `AddTimeWarpState()`. Probably `ISender` in the namespace `TimeWarp.Mediator` sends an action (not verified).
- Not found in the docs of 12: how states are discovered, the options of `AddTimeWarpState`, and how to send an action or read a state outside Blazor. Try, let the developer build, and adjust from the errors. Not verified: the lifetime of the store (per circuit in Blazor Server). Sources: the NuGet page, the GitHub README, release notes and migration page of TimeWarp.State.
- The proof (11.0.3, passed): `FantasyInformator.State` holds `LeagueListState`, `AddLeagueAction` and its handler. `FantasyInformator.State.Tests` has one test that uses no Blazor. In 11.0.3 a plain service container needs `services.AddTimeWarpState(x => x.Assemblies = [typeof(LeagueListState).Assembly])`, because by default only the calling assembly is scanned. Reading a state outside a component works with `IStore.GetState<T>()`. Sending the action used MediatR's `IMediator`, which is the reason for the move to 12; the proof must be rewritten for 12 until its test passes.

## Update flow

- The front end sends one action (for example `UpdateLeagues`). Its handler in the State project asks a service for application models (the service fetches, maps and returns). The handler matches each model against the state, adds new ones and updates known ones, and replaces placeholder links (models that hold only a key) by the real records. The same pattern loads the stored data from the database when the overview opens, with every item marked Unchanged.
- **Matching:** the state matches a complete object, and everything in it, by `ExternalId`. The exceptions are the links marked as possibly having no record: the owner of a fantasy team and the player of a rostered player. A fantasy team is unique by its league instance plus its `ExternalId` (the roster number repeats in every league).
- **Unknown player on a roster (to be implemented with the update flow):** `RosteredPlayer` stores the player as a Guid (`PlayerId`), not as the Sleeper id, and the database has no foreign key on it. So when a roster names a player the state does not know, the state creates a `Player` with that Sleeper id as `ExternalId`, a new Guid and an empty name, marked New. The roster entry points at that Guid. When the players list is fetched later, it matches that record by `ExternalId` and fills in the name, team and positions. The roster entry keeps pointing at the same Guid, so nothing is re-linked.
- **Unknown owner (to be implemented with the update flow):** the same. When a team names an owner the state does not know, the state creates an `Owner` with that Sleeper id as `ExternalId` and an empty name, marked New. The league-users update fills the name later.
- **Team without owner:** Sleeper can give a roster with no `owner_id`. `FantasyTeam.OwnerId` is `Guid?` in the Models and the Database models, and the team simply has no owner. The developer still has to create a migration (the column becomes nullable), with the EF commands in the Database README.
- **Player no longer on a roster:** when an update no longer lists a player on a fantasy team's roster (dropped or traded), the `RosteredPlayer` is marked Deleted. It shows in the report as removed, and Save deletes that one row from `RosteredPlayers`. The player record is never deleted. There is no "left on" field, and none is wanted.
- **Same league across seasons:** a league's `previous_league_id` is the id of the same league in the previous season; it is empty or "0" for the first season. Source: community write-ups and code that walks the chain, not Sleeper's official page (not read); confirm in a live response. The league instance of a new season is linked to the `League` of the league instance whose `ExternalId` equals that id. Name matching is dropped. When that league is not in the database (for example last year was never fetched), the state creates a new `League` and the history starts there.

## Errors and HTTP clients

- Every failure throws. Each of Sleeper, Nflverse and Database throws its own exception type, holding what failed (call name, HTTP status or operation). Only the front end catches and handles errors. No automatic retries. HTTP calls have a timeout. The exception types are not written yet.
- The HTTP clients are written by hand with `IHttpClientFactory` (the package `Microsoft.Extensions.Http` is in the Sleeper project) and `System.Text.Json`, not Refit. Sample replies in tests are inline strings. This follows the rule "Prefer Microsoft packages".

## Tests

- Every new class gets tests. `DatabaseSaveService` and `LeagueReadService` get tests too: the HTTP clients with a fake HTTP handler, the Database services with in-memory SQLite. This follows the rule "Keep tests in step with changes".
- The project settings stay in each `.csproj` (no `Directory.Build.props`). Warnings-as-errors comes on when the first clean build shows 0 warnings.
- `TestResults/` is ignored in git.

## Change tracking

- Every application model has a tracking state (`TrackingState`: New, Pending, Unchanged, Deleted) (`TrackedModel`). The exception is `LastSleeperCall`, which has none. The flag stays on the item itself. The database models have no state: a record read from the database is mapped as Unchanged. Instead, every database model holds a created time and an updated time, for database administration only. They are not mapped to the application models.
- The pending value is not part of the application models or the database models. It belongs to the state model, which is built when the app state is worked on. That model holds, per item, the current value, the pending value, the tracking state and the updated time. Only the flag of the pending (fetched) copy counts; the current (stored) copy is always Unchanged.
- Each item holds a current value and, only when it differs, a pending value. The current value is always Unchanged. The pending value is never equal to the current value.
- A New item has only a pending value and stays New through further updates until it is saved.
- A Deleted item has a pending value identical to the current one, with its state set to Deleted.
- A second update overwrites the pending value. A pending value equal to the current one is dropped.
- Items with a pending value get a slightly different color. A report icon button on a league or a player compares current with pending.
- After a save, the pending value becomes the current value and is cleared.
- Save writes only New, Pending and Deleted objects, one statement per changed object. A parent update does not update unchanged children. Unchanged objects are never sent to the database. Absence from a save never deletes anything. Only an object marked Deleted is deleted, and a delete cascades. A New complex object inserts records in all its tables.
- The save takes a flat list of changed models and writes them in one transaction. If one fails, none is written, and the error goes up. A model type the save does not know throws. Entity Framework only writes: it is told per object to insert, update or delete, and it does not detect changes. `CreatedUtc` is set on insert only, `UpdatedUtc` on insert and update, from the system clock, one time per save.
- Deletes: players, NFL teams, seasons, leagues, league instances, fantasy teams and owners are never deleted. Only rostered players, league slots, slot eligibility and player positions are. No table points to those, so a delete cascades to nothing. The cascade and restrict settings on the links stay as a safety net. If a game or game appearance ever has to be deleted (a cancelled game, a player listed by mistake), it is decided then.
- Every object has an updated time, set when that object itself changes. An update that finds no differences changes nothing, and Save stays disabled.
- Exception: the global lists (the players list, and later games) always get a new update time on an update. The players list is saved at once ("Rules for the whole application").
- `Season.CurrentWeek` (`int?`): the current week, only set on the current season (null on every other), from the `state/nfl` call. It is not stored, so the Database copy of the diagram does not show it. In the Models diagram a key column `get` marks a value that is not stored.

## Overview screen

- Tabs per season: the current year first, then earlier years in descending order.
- Each tab shows the leagues of that season as styled divs. No grid or table.
- Each league shows how long ago it was updated, an Update button for its contents (players), a Save button (disabled when nothing changed) and a report icon.
- Each tab has an Update button for the leagues of that tab's year. It calls only the user-leagues endpoint, with no players, scores or records.
- Next to it, a checkbox "attempt to get next season": take the last known year plus one and try to get leagues. If there are some, create a new season record and show a new tab for it.
- A league that already exists (matched by id) is only changed when its name differs.
- The report icon on a league or a player opens a modal with a Close button. It shows a summary of the changes. Each line is an accordion, and clicking it shows the full details: each changed field as the current value next to the pending value. A New item shows as added, a Deleted item as removed. A league's report also covers its changed players.
- The front end is not designed in more detail yet, except that errors are caught and handled there. Settle it in its own short session.

## Slots and positions

Slots and positions are tables, not enums. They are built as models (see the Models README).

- `Slot`: the Sleeper code, a name and an abbreviation. `LeagueSlot` links a league instance to a slot and holds how many of that slot the league has. Only slots that exist in the league are linked. `RosteredPlayer` holds the slot a player is in right now.
- `Position`: a name and an abbreviation. A player has one or more positions (`PlayerPosition`).
- `SlotEligibility` links a position to the slots it can start in (one or more) and holds `IsPreferred`: the position is preferred to start in that slot.
- When a player arrives from Sleeper with a position that is not known, the position is added. The front end shows a constant warning while a position has no slot link.
- Starting data (to be seeded when the database is created): the slots QB, RB, WR, TE, Flex, SuperFlex, K, DL, LB, IDP (Sleeper's `IDP_FLEX`), DB, BN, IR and TAX. The eligible slots by position: QB: QB, SuperFlex. RB, WR, TE: own slot, Flex, SuperFlex. K: K. DL, DE: DL, IDP. LB: LB, IDP. DB, CB, S: DB, IDP. Everyone: BN.
- A position can be dominant (`Position.IsDominant`): DL and DE are. A player who has a dominant position counts only as his dominant positions, and his other positions are ignored for the lineup. A player without one counts as all his positions. The eligible slots are then those of the positions that count. In a slot with a preferred position, such as LB in `IDP_FLEX`, the lineup picks the preferred position first.
- Every position fits BN, IR and TAX. Whether a player may use IR depends only on the injury status from Sleeper (`injury_status`: IR, PUP or Out), kept as one field, `Player.InjuryStatus` (the enum `InjuryStatus`: None, Q, D, OUT, IR, PUP). `IsIrEligible` is true for IR, PUP and OUT and is not stored. Q (questionable) and D (doubtful) are kept too, for display only: they never keep a player out of the lineup. Rookie years for TAX are not used. The slot Sleeper reports is the truth, even when it is not a preferred slot, for example a DB in IDP.

## First milestone data

- Roster: player names, eligible slots, the slot each player is in now, and how many slots of each kind the league has.
- Slot counts come from the league call: `roster_positions`, and `settings.taxi_slots` and `settings.reserve_slots`.
- A league's players update calls the rosters endpoint once. That call returns the rosters of all teams in the league, as player ids with the starters, IR and taxi lists. It returns no names or player details. All rosters are kept, not only the developer's own.
- Owners are a separate update (the league-users endpoint), which also gives the team names. A roster holds its owner id as a plain value.
- Player ids on a roster and owner ids on a roster are plain values with no database foreign key. They may point to a player or owner that is not known, for example an owner who left the league (see "Update flow").
- The players list (about 5 MB, at most once a day per the Sleeper docs) is a separate table in a global update section. It is fetched by hand, only when needed: once at the start, and again when rookies are added. The Update button is locked for 24 hours after a fetch. The time of the last fetch is kept in `LastSleeperCall`, one row per call name. The list is saved to the database immediately on fetch. Names and positions come from this list.

## Sleeper API facts (verified against docs.sleeper.com and live calls)

- Read-only and public. No token or credentials are needed.
- User: `GET https://api.sleeper.app/v1/user/<username>`
- Leagues: `GET https://api.sleeper.app/v1/user/<user_id>/leagues/nfl/<season>`
- League: `GET https://api.sleeper.app/v1/league/<league_id>`
- Rosters: `GET https://api.sleeper.app/v1/league/<league_id>/rosters`
- League users: `GET https://api.sleeper.app/v1/league/<league_id>/users`
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

Each project has its own settings class (`DatabaseSettings`, `SleeperSettings`) and checks it when the application starts. `appsettings.json` holds the default values for everything else. `appsettings.example.json` shows every key. Someone who puts their own values in `appsettings.json` has chosen that themselves. No path is written in code.

.NET loads user secrets by default only in the Development environment. Not verified for .NET 10 or 11.

## Open

- `IFantasyTeamService.GetLeagueUsersAsync` and `GetRostersAsync` still return Sleeper models. Decide what they return when the Sleeper service is built. The league users call gives owners and team names; the rosters call gives rosters and players; a `FantasyTeam` needs both. Existing mappers: `FantasyTeamMapper.ToOwner`, `ToFantasyTeam(roster, leagueUser)`, `ToRosteredPlayers(roster, rosterPositions)`. The rosters mapper needs the league's `roster_positions`.
- Confirm in a live response that `previous_league_id` behaves as described in "Update flow".
- The per-player points in the matchups call (`players_points`) are not in the Sleeper docs. Check them in a live response.
- `NflTeam.Name` is not given by Sleeper (only an abbreviation): seed data, given by the developer.
- The other links that may lack a record, besides the owner of a team and the player of a roster, are not decided.
- The state model with the pending values (see "Change tracking") is not built. It lives in the State project.

## Next steps

1. **Developer:** build the solution and run all tests, and paste warnings and failures. Fix what breaks (new State projects, interface changes, `Season.CurrentWeek`, nullable `OwnerId`). Open both `models.drawio.svg` files in draw.io and save them once, to check and redraw them (the Season row and the `OwnerId` type were edited by hand).
2. **Move to TimeWarp.State 12 and .NET 11**, in small steps, the developer builds after each: confirm the .NET 11 SDK is installed; all projects from `net10.0` to `net11.0`; `TimeWarp.State` in the App and State projects to `12.0.0-beta.17`, and the App also gets `TimeWarp.State.Blazor`; rewrite the State proof for 12 until its test passes; then check the rules that mention .NET 10 (for example the configuration rule).
3. **Developer:** create the migration for the nullable `OwnerId`, with the EF commands in the Database README, when ready.
4. Update `CLAUDE.md`: the developer agreed that the next agent writes the decisions and steps of this spec into the section "Transition between sessions" (State, Next steps, open points), replacing what is out of date. This counts as a direct `CLAUDE.md` instruction. Keep it short: the developer said `CLAUDE.md` will later be emptied or hold only rules for future changes. Add the State, Nflverse and Migrator projects to the root README.
5. Clean the rule files further: project names in `test.local.md` and the TimeWarp mention in "Banned packages" are project-specific and belong in this spec. The reference copy `.claude/coding-resources/.editorconfig` still holds folder names of the old solution (lines 231, 235, 239).
6. Sleeper and Nflverse HTTP clients, by hand, starting with the user-leagues call: exception types, timeout, fake HTTP handler tests with inline JSON.
7. Database: the read and save services with tests (in-memory SQLite), and the seed data (slots, positions, slot eligibility, NFL team names, given by the developer). The first migrations exist; new ones only when the developer asks.
8. State project: change the references as described under "Projects" (the App loses its references to Database and Sleeper, State gains them and one registration method). Then the state model with current and pending values, the update flow, the database load, the save handler, and the warning while a position has no slot link.
9. Overview screen in the Blazor project: season tabs, update and save buttons, report modal.
10. **Developer:** finish the Azure SQL database and the user secrets (`Database:ConnectionStringApp`, `Database:ConnectionStringEF`, `Sleeper:Username`, `Database:FailedSaveQueueFolder`). Claude never reads them.

## For the future

Each item gets its own mini-grill when it is started.

- Drafting, scouting and lineup management.
- Fantasy dynamics: player status, taxi squad, IR rules (`reserve_allow_*`, `taxi_years`, `taxi_allow_vets`).
- Lineup logic with preferred slots (for example IDP filled with LBs first, other players only when there are not enough LBs).
- Per-game scoring history for players. The current year is read by default, and a full-history command is separate. Only missing weeks are read. A per-week reload button exists for score corrections. An unchanged result stays unchanged.
- NFL team schedules on their own screen, used to know which weeks a player played. A traded player needs the team he played for each week.
- Wins and losses and matchups (a matchup is between two rosters in the same league). Records are defined when matchups are fetched.
- Claude in the application: reads the stored data, gathers its own data and makes recommendations (Anthropic API, separate billing).
- Failed saves: the only retry is for a save of data that cannot be fetched again until a timeout has passed (for example the players list, locked after a fetch). When it fails, it is written as a JSON file in a queue folder. Name the files by UTC timestamp down to microseconds so the names are unique. A retry button processes the queued files in chronological order. The folder comes from configuration in user secrets. The database is not expected to be offline much.
- Keeping the raw response of calls that can only be made once in a while (for example the players list, once a day): the raw response is written to a folder. When new data comes in, it is written too, and one backup of the previous file is kept. When the call is not possible, the latest file is read instead. The folder comes from configuration in user secrets. This is not the current focus.
- Finding players to pick up: AI searches the internet for available players, and the stored rosters of all teams in a league show which of them are already taken. A player who is on no team in a league (a free agent: all players minus the rostered ones) must also be scorable for that league. For example, when a running back is needed, AI suggests good players that are still free, and the application fetches their scoring. The application cannot browse the internet itself, so it needs a source it can call. Whether Sleeper has one (for example stats or trending players) is not verified.
- Comparing team rosters, for example to find which team is heavy in a position when looking for a trade.
- Putting the application online, with security.
- Other data sources if Sleeper lacks something.
