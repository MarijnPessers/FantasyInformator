# FantasyInformator (Blazor)

The front end. It shows state and starts actions. It holds no retrieval, database or mapping code.

- Blazor Server.
- State: TimeWarp.State. Whether its actions and handlers can live in another project is not verified.
- Every page has a code-behind. The markup holds no logic except loops and `if` on booleans.
- The overview has one tab per season, current year first. Leagues are styled divs.
- Opening the overview loads the stored seasons, leagues and rosters from the database. Sleeper calls and saves need a button.
- The report icon on a league or player opens a modal with the changes, current value next to pending value.
- The browser warns when you leave with unsaved changes.

References `FantasyInformator.Models`, `FantasyInformator.Sleeper` and `FantasyInformator.Database` (the last two to register their services).

Settings that belong to a person or PC come from .NET user secrets: the Sleeper username, the database connection string and the failed-save queue folder.
Tests: `FantasyInformatorTests`.
