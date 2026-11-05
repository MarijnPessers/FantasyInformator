# FantasyInformator.Sleeper

Retrieval of information from Sleeper. It has its own models for what Sleeper returns, and mappers (written by hand, no AutoMapper) from those to the application models.

## Technical notes

- References `FantasyInformator.Models`. Uses `Microsoft.Extensions.Http` for the HTTP clients.
- The Sleeper username is read from `Sleeper:Username` (required, checked at start by `AddSleeperOptions`).
- Nothing is called unless a button is pressed. Before the first Sleeper call of a session, `state/nfl` is called. The season and week are kept in state and never saved.
- The leagues update only calls the user-leagues endpoint and keeps what it returns. It fetches no players, scores or records.
- A league's players update calls the rosters endpoint once. That returns the rosters of all teams in the league as player ids, with no names or details, and all rosters are kept.
- Owners are a separate update that calls the league-users endpoint. Owner ids and player ids on a roster are plain values, because they can point to an owner or player that is not known.
- The players list is fetched by hand, only when needed (once at the start, and when rookies are added), and it is locked for 24 hours after a fetch.
- If a call fails, the rest of the application must keep working. This matters most for the unofficial endpoints.

## Sleeper endpoints

Official, read-only and public. No token is needed. Stay under 1000 calls a minute.

| Purpose | Call |
|---------|------|
| User | `GET https://api.sleeper.app/v1/user/<username>` |
| Leagues of a season | `GET https://api.sleeper.app/v1/user/<user_id>/leagues/nfl/<season>` |
| League | `GET https://api.sleeper.app/v1/league/<league_id>` |
| Rosters of all teams | `GET https://api.sleeper.app/v1/league/<league_id>/rosters` |
| League users (owners) | `GET https://api.sleeper.app/v1/league/<league_id>/users` |
| Players | `GET https://api.sleeper.app/v1/players/nfl` (about 5 MB, at most once a day) |
| State | `GET https://api.sleeper.app/v1/state/nfl` |
| Matchups of a week | `GET https://api.sleeper.app/v1/league/<league_id>/matchups/<week>` (the per-player points in it are not in the docs, not yet seen live) |

Unofficial and undocumented, so they can change or be blocked:

| Purpose | Call |
|---------|------|
| Schedule | `GET https://api.sleeper.com/schedule/nfl/regular/<season>` |
| Weekly stats | `GET https://api.sleeper.com/stats/nfl/<season>/<week>?season_type=regular&position[]=<position>` |

## Services

One service per model group that has a Sleeper call. Each method documents its call, what it gives to which model, and what Sleeper returns that is not used. Only the interfaces exist so far. The Sleeper models in `Services/Models` hold only the fields that are used. They have no `Sleeper` prefix, because the namespace says where they come from. Some names equal an application model (`League`, `Player`, `Game`), so a mapper needs a namespace alias. The Lineup and Administration groups have no call: slots and eligibility are one-time configuration, and `LastSleeperCall` is the application's own record.

| Service | Group | Calls |
|---------|-------|-------|
| `ILeagueService` | League | leagues of a user, league |
| `IFantasyTeamService` | FantasyTeam | user, league users, rosters |
| `IPlayerService` | Player | players, weekly stats (gives `GameAppearance`) |
| `INflService` | Nfl | state, schedule |
| `IScoringService` | Scoring | matchups of a week (gives the points per player) |

## Mappers

The `Mappers` folder holds static classes, one per service group, that map the Sleeper models to the application models by hand: `LeagueMapper`, `FantasyTeamMapper`, `PlayerMapper`, `NflMapper` and `ScoringMapper`.

- A link to another record is a stub that holds only its key (`ExternalId`, or `Abbreviation` for a position, or `Year` for a season). A later step swaps the stub for the real record.
- A mapper does not set `Id`, `State` or `UpdatedUtc`. That belongs to the update.
- A mapper that cannot read its input throws, for example a season that is not a number. The caller keeps the application working.
- A starter's slot follows from its place in `roster_positions` (without the bench). That `starters` follows that order is not verified.
- The schedule mapper sets the kickoff to midnight UTC of the game day, until the time from nflverse replaces it.

Known gaps in what Sleeper gives:

- `NflTeam.Name`: Sleeper gives only the team abbreviation.
- `Game.KickoffUtc`: the schedule gives the day, not the time. The time of day comes from the nflverse schedule (`FantasyInformator.Nflverse`).

Tests: `FantasyInformator.SleeperTests`.
