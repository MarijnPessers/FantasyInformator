# FantasyInformator (Blazor)

The front end of the application. It shows state and starts actions. It holds no retrieval, database or mapping code.

## Technical notes

- Blazor Server on .NET 10, with interactive server components.
- State management: [TimeWarp.State](https://www.nuget.org/packages/TimeWarp.State) 11.0.3, previously called Blazor-State. Whether its actions and handlers can live in another project is not verified yet. Check before moving them.
- Every page has a code-behind. The markup holds no logic except loops and `if` on booleans.
- Buttons use standard names (primary, secondary, warning, error and so on) with a matching color scheme. Update and report buttons use SVG icons.
- The overview shows one tab per season, current year first. Leagues are styled divs, not a grid or table.
- The browser warns when you leave with unsaved changes.
- When the overview opens, the stored seasons, leagues and rosters are loaded from the database. Everything else needs a button: Sleeper calls and saves. The one exception is the global players list, which is saved as soon as it is fetched.
- The report icon on a league or a player opens a modal with a summary of the changes. Each line is an accordion that shows the full details (current value next to pending value).

## Dependencies

References `FantasyInformator.Models`, `FantasyInformator.Sleeper` and `FantasyInformator.Database`. The last two are referenced to register their services at startup.

## Configuration

Anything specific to a person or a PC comes from .NET user secrets, which override `appsettings.json`: the Sleeper username, the database connection string and the folder of the failed-save queue. `appsettings.json` holds defaults and `appsettings.example.json` shows every key.

Tests: `FantasyInformatorTests`.
