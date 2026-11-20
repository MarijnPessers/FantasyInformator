# Players update

The first thing to build. Sleeper allows the players call only once every 24 hours, so fetched players must never be lost when the database has a hiccup. Only data from such limited calls goes through a file. Any other save that fails is fetched again.

How Database keeps the players safe is described in `FantasyInformator.Database/README.md`. Read it together with this spec.

## Flow

Fetching (`FetchAllPlayersAction`):

1. The front end sends the action to fetch the players.
2. The State handler calls `FetchAllPlayersAsync` in Logic.
3. Logic asks Database for the last fetch.
4. The last fetch was less than 24 hours ago: nothing is fetched, and Logic returns all players.
5. The last fetch was 24 hours ago or longer, or there was none: Logic fetches the players, hands them to Database to save, and returns all players. When the fetch fails, Logic returns the players it already has.
6. State replaces its players with the list and sets them all to `Unchanged`: they're already saved or waiting in the file, so no Save follows. When Logic returns `null`, State keeps its players.

Loading (`LoadAllPlayersAction`) works the same way, without fetching: the handler calls `LoadAllPlayersAsync`, and Logic returns all players. When State already holds players, the handler skips Logic, unless the action is sent with `Override: true`. Fetching has no such check.

Logic always returns all players. An empty list only means there are no players yet.

## Progress reports

- Progress reports work as `conventions.spec.md` describes.
- The progress state lives in State, only for the front end, and holds the reports. A snackbar component listens to it and shows each new report that passes the display level.
- Reports are shown in `CreatedUtc` order. On a tie, the one that came in first stays first.

## Tests

- Unit tests for each new class, as usual.
- The Database tests follow the scenarios in `FantasyInformator.Database/README.md`.
- Flow tests in `FantasyInformator.Tests` run the real State and Logic code, with stand-ins for everything below them:
  - NSubstitute for the Sleeper player service, with mock player data.
  - NSubstitute for the Database services, which can return `false` and report a failed save.
  - `Microsoft.Extensions.DependencyInjection` for the real registrations.
- One flow test per scenario:
  - Last fetch 24 hours ago or longer: fetched, handed to Database, and the players end up in State.
  - Last fetch less than 24 hours ago, from the database: nothing fetched, and the players from the database end up in State.
  - Last fetch less than 24 hours ago, from a waiting file: nothing fetched, and the players from the file end up in State.
  - Database reports a failed save: the report reaches the progress state, and the players still end up in State.

## Needed first

- The Sleeper players call: `IPlayerService.GetPlayersAsync` has no implementation yet.
- The Database part, as described in `FantasyInformator.Database/README.md`.
- Shared, with `ProgressReport`.
- In State: the progress state, its `IProgress` class and the tracing pipeline behavior.
- In the App: the snackbar component.

## Open

Answer these before building anything from this spec.

- ⚠️ EF's InMemory provider ignores transactions, so a unit test can't prove the one-transaction save.
