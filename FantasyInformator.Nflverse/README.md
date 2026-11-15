# FantasyInformator.Nflverse

Gives the kickoff times that Sleeper lacks. Sleeper's schedule has the day of each game but not the time. This project reads the public nflverse schedule file.

- References `FantasyInformator.Domain`. Its own models hold only the columns used.
- One call returns all seasons. Other seasons are dropped after reading.
- The file's kickoff time is Eastern time. It is converted to UTC.
- Team codes can differ from Sleeper's.
- It is not an official source. If the call fails, the rest of the application keeps working.
- Nothing is called unless a button is pressed.
- Only the schedule service interface (schedule of one season) and the mapper exist so far. The service returns application models; the nflverse models stay inside the project.

The home and away team of a game are stubs that hold only the external id. Team codes are changed to Sleeper's. A kickoff time that does not exist in Eastern time throws.

No field of an nflverse model is required; each gets a default instead, such as an empty text. nflverse decides what the file holds, and a field that must be there would make reading the whole file fail when it is missing. The mapper checks instead: it throws when the home or away team code is empty, because an application model always needs it.

Source: the public nflverse games file on GitHub.
