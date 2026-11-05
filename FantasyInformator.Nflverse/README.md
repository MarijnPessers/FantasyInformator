# FantasyInformator.Nflverse

Retrieval of the kickoff times that Sleeper does not give. Sleeper's schedule has the day of each game but not the time of day. This project reads the public nflverse schedule file and gives the time of day.

## Technical notes

- References `FantasyInformator.Models`. It has its own models for what the file returns, in `Services/Models`, and they hold only the columns that are used. Mappers (written by hand) will map them to the application models.
- One call returns all seasons since 1999. The rows of other seasons are dropped after reading.
- The kickoff time in the file is Eastern time, whatever the time zone of the stadium. It is converted to UTC.
- Team codes can differ from Sleeper's (the Rams are `LA` here and `LAR` in Sleeper), so a mapping is needed to match the games.
- It is not an official NFL source and can change or disappear. If the call fails, the rest of the application must keep working.
- Nothing is called unless a button is pressed.
- Only the interface and the mapper exist so far.

## Mappers

`NflScheduleMapper` in the `Mappers` folder maps a row to a `Game`: the week, the kickoff in UTC, and a stub for the home and away team that holds only the `ExternalId`. A team code that differs from Sleeper's is changed to Sleeper's (only `LA` to `LAR` so far). A kickoff time that does not exist in Eastern time throws.

## Service

| Service | Group | Call |
|---------|-------|------|
| `INflScheduleService` | Nfl | schedule of one season |

## Endpoint

| Purpose | Call |
|---------|------|
| Schedule of all seasons | `GET https://raw.githubusercontent.com/nflverse/nfldata/master/data/games.csv` |

Tests: `FantasyInformator.NflverseTests`.
