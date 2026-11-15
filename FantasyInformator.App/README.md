# FantasyInformator.App (Blazor)

The front end. It shows state and starts actions. It holds no retrieval, database or mapping code.

- Blazor Server.
- State: TimeWarp.State. Whether its actions and handlers can live in another project is not verified.
- Every page has a code-behind. The markup holds no logic except loops and `if` on booleans.
- The overview has one tab per season, current year first. Leagues are styled divs.
- Opening the overview loads the stored seasons, leagues and rosters from the database. Sleeper calls and saves need a button.
- The browser warns when you leave with unsaved changes.

References `FantasyInformator.Domain` and `FantasyInformator.State` (the latter to register the services).

Settings that belong to a person or PC come from .NET user secrets: the Sleeper username, the database connection string and the failed-save queue folder.

## Styling

- All CSS comes from Tailwind. Write it in `Styles/tailwind.css`. Each build turns that file into `wwwroot/tailwind.css`, which is never edited by hand.
- The first build downloads the Tailwind program (about 110 MB) into `.tailwind/`.
- With hot reload, a new Tailwind class only shows after the next build. To see it right away, run this in the App folder: `.tailwind\v4.3.5\tailwindcss-windows-x64.exe -i Styles\tailwind.css -o wwwroot\tailwind.css --watch`.
- To update Tailwind, change `TailwindVersion` in the project file.
