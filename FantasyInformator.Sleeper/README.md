# FantasyInformator.Sleeper

Retrieval from Sleeper. It has its own models for what Sleeper returns and hand-written mappers to the application models. Only the service interfaces exist so far.

- References `FantasyInformator.Models`. Uses `Microsoft.Extensions.Http`.
- The username is read from `Sleeper:Username` (required).
- Nothing is called unless a button is pressed. Before the first call of a session, `state/nfl` gives the season and week. They are kept in state and never saved.
- The leagues update calls only the user-leagues endpoint. The players update calls the rosters endpoint once, which gives player ids only. Owners come from the league-users endpoint.
- The players list is fetched only when needed and locked for 24 hours after a fetch.
- If a call fails, the rest of the application keeps working.

## Sleeper endpoints

Official, public, no token. Stay under 1000 calls a minute.

| Purpose | Call |
|---------|------|
| User | `GET https://api.sleeper.app/v1/user/<username>` |
| Leagues of a season | `GET https://api.sleeper.app/v1/user/<user_id>/leagues/nfl/<season>` |
| League | `GET https://api.sleeper.app/v1/league/<league_id>` |
| Rosters | `GET https://api.sleeper.app/v1/league/<league_id>/rosters` |
| League users | `GET https://api.sleeper.app/v1/league/<league_id>/users` |
| Players | `GET https://api.sleeper.app/v1/players/nfl` (about 5 MB, once a day) |
| State | `GET https://api.sleeper.app/v1/state/nfl` |
| Matchups of a week | `GET https://api.sleeper.app/v1/league/<league_id>/matchups/<week>` (per-player points not in the docs, not yet seen live) |

Unofficial, so they can change or be blocked:

| Purpose | Call |
|---------|------|
| Schedule | `GET https://api.sleeper.com/schedule/nfl/regular/<season>` |
| Weekly stats | `GET https://api.sleeper.com/stats/nfl/<season>/<week>?season_type=regular&position[]=<position>` |

## Services

| Service | Calls |
|---------|-------|
| `ILeagueService` | leagues of a user, league |
| `IFantasyTeamService` | user, league users, rosters |
| `IPlayerService` | players, weekly stats |
| `INflService` | state, schedule |
| `IScoringService` | matchups of a week |

The Sleeper models in `Services/Models` hold only the fields that are used. Some names equal an application model, so a mapper needs a namespace alias.

## Mappers

Static classes in `Mappers`, one per service: `LeagueMapper`, `FantasyTeamMapper`, `PlayerMapper`, `NflMapper`, `ScoringMapper`.

- A link to another record is a stub that holds only its key. A later step swaps it for the real record.
- A mapper does not set `Id`, `State` or `UpdatedUtc`.
- A mapper that cannot read its input throws.
- A starter's slot follows from its place in `roster_positions`. Not verified.
- The schedule mapper sets the kickoff to midnight UTC of the game day, until nflverse gives the time.

Gaps: `NflTeam.Name` (Sleeper gives only the abbreviation) and the kickoff time of a game (see `FantasyInformator.Nflverse`).

Tests: `FantasyInformator.SleeperTests`.
