# FantasyInformator.Sleeper

Retrieval from Sleeper. It has its own models for what Sleeper returns and hand-written mappers to the application models. Only the service interfaces exist so far.

- References `FantasyInformator.Models`.
- The username comes from the user secrets (required).
- Nothing is called unless a button is pressed. Before the first call of a session, the NFL state call gives the season and week. They are kept in state and never saved.
- The leagues update calls only the user-leagues endpoint. The players update calls the rosters endpoint once, which gives player ids only. Owners come from the league-users endpoint.
- The players list is fetched only when needed and locked for a while after a fetch.
- If a call fails, the rest of the application keeps working.

## Sleeper endpoints

Official, public, no token. Stay under the call limit per minute. The players call returns a large answer, so it is fetched rarely. The per-player points in the matchups call are not in the docs and not yet seen live.

The schedule and weekly stats calls are unofficial, so they can change or be blocked.

## Services

| Service | Calls |
|---------|-------|
| League service | leagues of a user, league |
| Fantasy team service | user, league users, rosters |
| Player service | players, weekly stats |
| NFL service | state, schedule |
| Scoring service | matchups of a week |

The Sleeper models hold only the fields that are used. Some names equal an application model, so a mapper needs a namespace alias.

## Mappers

- A link to another record is a stub that holds only its key. A later step swaps it for the real record.
- A mapper does not set the id, the state or the update time.
- A mapper that cannot read its input throws.
- A starter's slot follows from its place in the roster positions list. Not verified.
- The schedule mapper sets the kickoff to midnight UTC of the game day, until nflverse gives the time.

Gaps: the NFL team name (Sleeper gives only the abbreviation) and the kickoff time of a game (see `FantasyInformator.Nflverse`).

Tests: `FantasyInformator.SleeperTests`.
