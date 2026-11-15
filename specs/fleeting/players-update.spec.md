# Players update

The first thing to build. Sleeper allows the players call only once every 24 hours, so fetched players must never be lost when the database has a hiccup. Only data from such limited calls goes through a file. Any other save that fails is fetched again.

## The players file

- Database owns the players file: it writes it, retries it and reports on it. Logic only decides when to fetch.
- The folder is `DatabaseSettings.FailedSaveQueueFolder`, set in `appsettings.json`.
- When Database is asked to save players, it first writes them to a file with the `LastSleeperCall` time. The file holds the Domain players, because those are what Database gets.
- Each file name ends in the UTC time of the fetch, down to the second, so a new file never overwrites an older one.
- When the file can't be written, Database reports it and still tries the insert. The file is only a backup.
- After the new file is written, and before the insert, older players files are deleted.
- The players and the `LastSleeperCall` time are saved in one transaction: both are saved, or neither is.
- When the insert succeeds, the file is deleted and the save returns `true`. When it fails, the file stays and waits, Database reports that the save is pending, and the save returns `false`.

## A waiting file

- Before every database call, Database checks for a waiting players file and tries to write it into the database.
- Every attempt is reported through `IProgress`, and so is its result.
- When the write fails, the original database call still goes ahead.
- When that write fails, the file is the latest version of the players: every read of the players or of the last fetch gets them from the file.

## Flow

1. The front end sends the action to update the players list.
2. The State handler calls Logic.
3. Logic asks Database for the last fetch. Database first tries to write a waiting file, as above.
4. The last fetch was less than 24 hours ago:
   - It came from the database: nothing is fetched, and Logic returns an empty list. The players are already loaded.
   - It came from a waiting file: Logic returns the players from the file.
5. The last fetch was 24 hours ago or longer, or there was none: Logic fetches the players, hands them to Database to save, and returns them.
6. State updates its players. They're already saved or waiting in the file, so their tracking state doesn't change and no Save follows.

## Progress reports

- Progress reports work as `conventions.spec.md` describes.
- The progress state lives in State, only for the front end, and holds the reports. A snackbar component listens to it and shows each new report that passes the display level.
- Reports are shown in `CreatedUtc` order. On a tie, the one that came in first stays first.

## Tests

- Unit tests for each new class, as usual.
- Database tests cover the file, with `MockFileSystem` from `TestableIO.System.IO.Abstractions.TestingHelpers` and EF's InMemory provider. Database uses `IFileSystem` from `TestableIO.System.IO.Abstractions.Wrappers`. One test per scenario:
  - The save succeeds: file written, older files deleted, inserted, file deleted.
  - The insert fails: the file stays, a pending save is reported, and the save returns `false`.
  - The file can't be written: it's reported, and the insert is still tried.
  - File waiting, database back: the file is written and deleted, then the call goes ahead.
  - File waiting, database down: the attempt and its failure are reported, and the call goes ahead.
  - File waiting and its write fails, then a read of the players or of the last fetch: they come from the file.
- A Database test makes the database fail with a stand-in: an InMemory context with a `SaveChangesInterceptor` that throws on save.
- Flow tests in `FantasyInformator.Tests` run the real State and Logic code, with stand-ins for everything below them:
  - NSubstitute for the Sleeper player service, with mock player data.
  - NSubstitute for the Database services, which can return `false` and report a pending save.
  - `Microsoft.Extensions.DependencyInjection` for the real registrations.
- One flow test per scenario:
  - Last fetch 24 hours ago or longer: fetched, handed to Database, and the players end up in State.
  - Last fetch less than 24 hours ago, from the database: nothing fetched, and an empty list comes back.
  - Last fetch less than 24 hours ago, from a waiting file: nothing fetched, and the players from the file end up in State.
  - Database reports a pending save: the report reaches the progress state, and the players still end up in State.

## Needed first

- The Sleeper players call: `IPlayerService.GetPlayersAsync` has no implementation yet.
- A Database read of the last fetch that falls back to a waiting file.
- A Database save of the players that writes the file and saves the players and the `LastSleeperCall` time in one transaction. `DatabaseSaveService.SaveAsync` only takes tracked models, and `LastSleeperCall` isn't one.
- Shared, with `ProgressReport`.
- In State: the progress state, its `IProgress` class and the tracing pipeline behavior.
- In the App: the snackbar component.

## Open

Answer these before building anything from this spec.

- ⚠️ EF's InMemory provider ignores transactions, so a unit test can't prove the one-transaction save.
