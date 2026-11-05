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
| League users (owners) | `GET https://api.sleeper.app/v1/league/<league_id>/users` (not verified) |
| Players | `GET https://api.sleeper.app/v1/players/nfl` (about 5 MB, at most once a day) |
| State | `GET https://api.sleeper.app/v1/state/nfl` |

Unofficial and undocumented, so they can change or be blocked:

| Purpose | Call |
|---------|------|
| Schedule | `GET https://api.sleeper.com/schedule/nfl/regular/<season>` |
| Weekly stats | `GET https://api.sleeper.com/stats/nfl/<season>/<week>?season_type=regular&position[]=<position>` |

Tests: `FantasyInformator.SleeperTests`.
