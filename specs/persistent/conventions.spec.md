# Conventions

Only the coding decisions that the existing code, `CLAUDE.local.md`, the SonarQube, IDE and standard .NET rules, and the newest preferred way of writing C# don't already settle.

## Structure

- Project references:
  - Domain: nothing
  - Shared: nothing
  - Sleeper: Domain, Shared
  - Nflverse: Domain, Shared
  - Database: Domain, Shared
  - Logic: Domain, Shared, Sleeper, Nflverse, Database
  - State: Domain, Shared, Logic
  - App: Domain, Shared, State
  - Migrator: Database
- `FantasyInformator.Shared` is the application's own library of shared functionality, such as the progress report.
- The App tells State what to change. State holds the state and little logic: it matches fetched data to what it holds by `ExternalId` and sets the tracking state. Its handlers call Logic for everything else.
- Logic holds the commands as reusable methods. It fetches data from Sleeper and Nflverse, and it is the only project that reads and writes the database and decides when to save. It never changes data or its tracking state; State does that.
- Database keeps the saves of limited calls (such as the once-a-day players call) safe in a file, retries them and reports on them through `IProgress`.
- Each project has one extension method that registers its services. It also calls the extension method of each project it uses and passes the configuration on, so `Program.cs` only calls the App's.
- Mappers are static and pure.
- Each project that needs configuration has its own settings class and its own section in `appsettings.json`, named after the project. Settings that belong to no single project stay outside those sections. Values specific to a person or PC go in user secrets. Settings are checked when the app starts.
- Project settings stay in each `.csproj` (no `Directory.Build.props`). Warnings-as-errors comes on after the first clean build with 0 warnings.

## State

- TimeWarp.State 12 (11.x depends on MediatR). Everything about it is in `timewarp-state.spec.md`.

## Errors, calls and timeouts

- Errors:
  - No project throws an expected failure to its caller.
  - Each project catches only the specific errors it expects, such as `HttpRequestException` or `DbException`, and reports them through `IProgress<ProgressReport>`. State sends the add-report action instead.
  - A call that runs out of its total time is an expected error: Polly's `TimeoutRejectedException` is caught and reported.
  - A catch of every error (`Exception`) is never used: an unexpected error is a bug and still throws, so it shows.
- A fetch, from a service or from the database, returns a list: empty when there's nothing, one or more items when there's data, `null` when it failed. A save (insert, update or delete) returns `true` or `false`. Logic answers State the same way.
- Progress reports:
  - Whatever reports gets `IProgress<ProgressReport>` injected. State registers it once per user session (scoped).
  - A class in State implements it. Each report sends the State action that adds it to the progress state.
  - Nothing waits for that action, so a report may arrive after the call that made it has returned.
  - When State gets `null` or `false` back, it can trust that a report about it has arrived or will arrive. It decides per case whether to stop.
  - State reports by sending that action itself.
- A progress report holds a severity (`LogLevel`), a header, a message, whether to show it to the user, and the exception, if there is one. It sets its own `CreatedUtc`, from `DateTime.UtcNow`, and its own trace ID when it's made. A test can set `CreatedUtc` itself.
- The App shows a progress report when its severity is at or above the display level, or when it's marked to show to the user. The display level is an App setting and defaults to `Warning`.
- Tracing:
  - Each action a page sends gets its own trace. The behavior `TraceBehavior` runs around every action and starts an `Activity` for it, named after the action. A test checks that the State project declares it.
  - The activity is created directly (`new Activity(...).Start()`), because `ActivitySource` only creates one when something listens.
  - An action sent while no action is running starts a new trace, so it doesn't pick up the Blazor connection's activity. An action sent while another one runs stays in that trace.
  - The trace ID follows every async call by itself, parallel ones too. No method passes it as a parameter.
  - A progress report takes the current trace ID and activity name when it's made.
  - The App can group reports by trace ID. How it shows a group is decided later.
- Every call to a service or the database is async and takes a `CancellationToken`. Steps that need each other's result run one after another, and the rest may run in parallel. After a failure, Logic decides per feature whether the action stops or goes on.
- Web clients are written by hand with `IHttpClientFactory` and `System.Text.Json`, without Refit.
- Retries:
  - Web calls retry through `Microsoft.Extensions.Http.Resilience`, database calls through `Microsoft.Extensions.Resilience`. Both use Polly.
  - The app doesn't use EF's own retry.
  - No circuit breaker and no rate limiter: the app has one user.
  - Limited calls never retry, such as the players fetch and the players save.
  - Before each of its calls, Database tries to save a waiting players file into the database.
- After a timeout:
  1. The attempt's cancellation token is cancelled.
  2. The call is awaited until it ends.
  3. An answer still arrives: the cancel came too late, so that answer is used and nothing is retried.
  4. The call ends cancelled: the cancel was accepted, and the call is retried.
- Timeouts, each a setting:
  - A call: 30 seconds in total. The first attempt gets 10 seconds. As soon as its cancel is accepted, the single retry starts without waiting. The retry has no timeout of its own, so the 30-second total stops it.
  - Players fetch and save: 5 minutes, no retry.
  - Wake-up call: 2 minutes, no retry.
- `HttpClient`'s own 100-second timeout is turned off, so only these apply.
- At startup, the first call is a wake-up call to the database (`SELECT 1`). The app shows a loading screen and does nothing else until it answers.
- The wake-up call never retries. When it fails, the app shows an error message that the database isn't available.
- Database calls:
  - Each attempt wraps the whole call and gets its own context from the factory.
  - Database recognizes temporary SQL errors by its own list of SQL error numbers. EF's list (`SqlServerTransientExceptionDetector`) is internal API that may change without notice, so it isn't used.
  - SqlClient can report an accepted cancel as a `SqlException` ("Operation cancelled by user") instead of an `OperationCanceledException` (SqlClient issue #26). When the attempt's token was cancelled, Database treats that `SqlException` as an accepted cancel.
  - A save inserts a row only when its unique key isn't in the database yet, and updates it otherwise. So a retry of a save that already went through updates the row instead of failing.
  - The Migrator keeps EF's own retry.

## Classes

- A class uses its primary constructor parameters directly and never copies them into a field.
- A property that must always have a value is `required` and gets no default. A list may keep `= []`.
- Own models: a property is `required` only when it always has a value, stubs included. Sleeper and Nflverse answer models are never `required`.
- A mapper throws when a value for a `required` property is empty.
- Classes don't have to be sealed.

## Blazor

- Every component has a code-behind (`.razor.cs`).
- The template holds only `if` statements and loops. All other logic is in the code-behind.
- Services come in through the primary constructor, never through `[Inject]` properties.
- All CSS comes from Tailwind.
- A code-behind is ordered like this:
  - Class declaration: the attributes (`[Route]`, `[Authorize]`, `[StreamRendering]`), then the primary constructor with the services, the base class and the interfaces.
  - Members, top to bottom:
    1. `[Parameter]` properties, with `[EditorRequired]` ones first.
    2. `RenderFragment` parameters.
    3. `EventCallback` parameters.
    4. The `CaptureUnmatchedValues` parameter.
    5. `[CascadingParameter]` properties.
    6. `[SupplyParameterFromQuery]` properties.
    7. `[SupplyParameterFromForm]` properties.
    8. `[PersistentState]` properties.
    9. Constants.
    10. Static readonly fields.
    11. Private readonly fields.
    12. Private fields that hold changing state.
    13. `@ref` fields (element and child-component references).
    14. Properties the template reads (state, display values, the booleans for `if`).
    15. Other properties (such as a TimeWarp `GetState` accessor).
    16. Lifecycle methods, in the order Blazor calls them: `SetParametersAsync`, `OnInitialized`, `OnInitializedAsync`, `OnParametersSet`, `OnParametersSetAsync`, `ShouldRender`, `OnAfterRender`, `OnAfterRenderAsync`.
    17. Event handlers that the template calls.
    18. Public methods that a parent calls through `@ref`.
    19. `[JSInvokable]` methods.
    20. Private methods, in the order they're first used.
    21. `Dispose` or `DisposeAsync`.
    22. Nested types.
  - Within a group, members follow the order the template uses them.

## Texts

- Every text shown to the user, in any project, comes from a `.resx` file.
- English only: one `Texts.resx` per project, read through the generated `Texts` class, without `IStringLocalizer`.

## Mapping

- Mappings are written by hand.
- A project with its own models (Sleeper, Nflverse, Database) holds the mappers between those models and Domain, so no other project knows its models.

## Tests

- Each project except Domain has a test project with the same name plus `.Tests`. Domain holds only models and no logic, so it has no tests.
- `FantasyInformator.Tests` holds tests of chosen flows through State and Logic, with stand-ins for Sleeper and the database.
- Tests use MSTest and AutoFixture.
- Test method names follow `.editorconfig`: `Member_ShouldResult_WhenSituation`.
- A test project has the same folders as its project. A test class is named `<ClassName>Tests`.
- What several tests in one class share, such as the Fixture, a stand-in, the service under test or a service provider, is created in a method `Initialize` with `[TestInitialize]`, not in each test and not in a field initializer. What must be disposed is disposed in a method `Cleanup` with `[TestCleanup]`. Constants stay `const`.
- Stand-ins for interfaces come from NSubstitute.
- The clock is read once, with `DateTime.UtcNow`, and the value is passed on. A test passes its own time, written by hand or made by AutoFixture.
- The database is EF's InMemory provider.
- Web calls are answered by `RichardSzalay.MockHttp`.
- A web answer is a filled object turned into JSON. Each Sleeper model also gets one test that reads a small JSON text written by hand, to check the field names.
