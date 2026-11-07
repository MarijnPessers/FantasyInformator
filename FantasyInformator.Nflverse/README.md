# FantasyInformator.Nflverse

Gives the kickoff times that Sleeper lacks. Sleeper's schedule has the day of each game but not the time. This project reads the public nflverse schedule file.

- References `FantasyInformator.Models`. Its own models are in `Services/Models` and hold only the columns used.
- One call returns all seasons since 1999. Other seasons are dropped after reading.
- The file's kickoff time is Eastern time. It is converted to UTC.
- Team codes can differ from Sleeper's (`LA` here, `LAR` in Sleeper).
- It is not an official source. If the call fails, the rest of the application keeps working.
- Nothing is called unless a button is pressed.
- Only the interface `INflScheduleService` (schedule of one season) and the mapper exist so far.

`NflScheduleMapper` maps a row to a `Game`: the week, the kickoff in UTC, and a stub for the home and away team that holds only the `ExternalId`. `LA` becomes `LAR`. A kickoff time that does not exist in Eastern time throws.

Endpoint: `GET https://raw.githubusercontent.com/nflverse/nfldata/master/data/games.csv`

Tests: `FantasyInformator.NflverseTests`.
