# FantasyInformator.App (Blazor)

The front end. It shows state and starts actions. It holds no retrieval, database or mapping code.

- Blazor Server.
- State: TimeWarp.State. Whether its actions and handlers can live in another project is not verified.
- Every page has a code-behind. The markup holds no logic except loops and `if` on booleans.
- The overview has one tab per season, current year first. Leagues are styled divs.
- Opening the overview loads the stored seasons, leagues and rosters from the database. Sleeper calls and saves need a button.
- The browser warns when you leave with unsaved changes.
- `MainLayout` wakes the database first and shows a spinner with "Connecting to database". If it fails, it shows that the database isn't available.
- The app finds you in the list of owners by the name in `FocusOwnerName` (in `appsettings.json`, under `State`), for example `GoudaCheesewizz`. That owner is the focus owner.

References `FantasyInformator.Domain` and `FantasyInformator.State` (the latter to register the services).

Settings that belong to a person or PC come from .NET user secrets: the Sleeper username, the database connection string and the failed-save queue folder.

[[_TOC_]]

## Styling

- The site uses Tailwind. Write it in `.tailwind/tailwind.css`. Each build turns that file into `wwwroot/css/tailwind.css`, which is never edited by hand.
- The first build downloads the Tailwind program (about 110 MB) into `.tailwind/v4.3.5/`.
- With hot reload, a new Tailwind class only shows after the next build. To see it right away, run this in the App folder: `.tailwind\v4.3.5\tailwindcss-windows-x64.exe -i .tailwind\tailwind.css -o wwwroot\css\tailwind.css --watch`.
- To update Tailwind, change `TailwindVersion` in the project file.
- `wwwroot/css/spinner.css` holds the spinner's own styles and is written by hand.

## Spinners

While something runs, the app shows a dark layer over the whole page with a spinning football and a text under it, such as "Connecting to database" with dots that count 1, 2, 3.

- `Components/Layout/Spinner.razor` draws the layer. `MainLayout` holds it once, so it covers every page.
- It reads `SpinnerState` from State and shows while at least one spinner is active.
- When several spinners are active, it shows the text of the oldest one. A spinner without a text shows "Loading" (`DefaultSpinnerMessage` in `Texts.resx`).
- `wwwroot/css/spinner.css` keeps the layer invisible for its first 0.25 seconds, so a call that ends quickly never shows it. It also spins the football and counts the dots.

The App starts spinners in two ways. How both work, and how to add one, is in the State README, under "Spinners".

- An action that implements `ISpinnerAction` starts its own spinner, for example `LoadAllOwnersAction`.
- `sender.StartSpinnerAsync(text, cancellationToken)` starts one spinner around several calls. `MainLayout` wraps the wake-up call this way:

```csharp
await using var spinner = await sender.StartSpinnerAsync(Texts.ConnectingToDatabase, CancellationToken.None);
await sender.Send(new DatabaseState.DatabaseWakeUpAction(), CancellationToken.None);
```

"Connecting to database" shows until the wake-up call is done. The wake-up action also starts a "Loading" spinner of its own, but that one is newer, so it stays hidden.

## Toasts

Every progress report goes to `ProgressReportState` in State. `Components/Layout/Toaster.razor`, which `MainLayout` holds once, shows some of them as toasts at the top right of the page.

- A report shows as a toast when its level is at or above `DisplayLevel`, or when it's marked to show to the user (`ShowToUser`).
- A toast disappears after 15 seconds (`ToastDuration`), or when you click it.
- The color follows the report's level:

| Level | Color |
|---|---|
| Debug | gray |
| Information | light blue |
| Success | green |
| Warning | yellow |
| Error | red |

Both settings are in the `App` section of `appsettings.json`. `appsettings.json` sets `DisplayLevel` to `Warning`, so Debug, Information and Success reports stay hidden unless they're marked to show to the user. Without the setting, it's `Success`. To see every report while developing, set it to `Debug`:

```json
"App": {
  "DisplayLevel": "Debug",
  "ToastDuration": "00:00:10"
}
```
