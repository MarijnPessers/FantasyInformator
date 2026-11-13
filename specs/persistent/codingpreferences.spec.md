# Coding preferences

Only the coding decisions that the existing code, `CLAUDE.local.md`, the SonarQube, IDE and standard .NET rules, and the newest preferred way of writing C# don't already settle.

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

## Tests

- Test method names follow `.editorconfig`: `Member_ShouldResult_WhenSituation`.
- Stand-ins for interfaces come from NSubstitute.
- When time is tested, the code reads it through an injected `TimeProvider` and the test uses `FakeTimeProvider` from `Microsoft.Extensions.TimeProvider.Testing`.
- The database is EF's InMemory provider.
- Web calls are answered by `RichardSzalay.MockHttp`.
- A web answer is a filled object turned into JSON. Each Sleeper model also gets one test that reads a small JSON text written by hand, to check the field names.

<!--
# Coding preferences

## Log

### Q1 - What the file covers

Question: Should the file hold the rules no analyzer can check (structure, patterns, tests), plus each preference that `.editorconfig` turns on, with the `CLAUDE.md:` comments in `.editorconfig` pointing here instead?

Recommended: Yes.

Answer: It holds only answers on how to code that can't already be deduced from the existing source, from `CLAUDE.local.md`, from the SonarQube, IDE and standard .NET rules, or from the newest preferred way of writing code.

### Q2 - Test method names

Question: The `.editorconfig` comment says `Member_ShouldResult_WhenSituation`, but the existing tests use `Member_Situation_Result`. Should `Member_Situation_Result` be the rule, with the `.editorconfig` comment changed to match?

Recommended: Yes.

Answer: No. `.editorconfig` is leading, so the tests get renamed to `Member_ShouldResult_WhenSituation`. The rename goes into a rewrites spec that gets done after the grilling.

### Q3 - Primary constructor parameters

Question: `AddLeagueHandler` copies `store` into `private readonly IStore _store = store;`. Should a class use its primary constructor parameters directly, without copying them into a field?

Recommended: Yes.

Answer: Yes. A primary constructor parameter is private to the class; the only difference from a copy is that it isn't `readonly`. Don't copy it.

### Q4 - Blazor component code

Question: Should each component keep its C# in a separate `.razor.cs` partial class (code-behind), with only markup in the `.razor` file?

Recommended: Yes.

Answer: Yes, code-behind always.

### Added by Marijn - CSS

Answer: All CSS comes from Tailwind. Tailwind gets added to the solution.

### Q6 - How Tailwind gets built

Question: No Node.js on the PC. Options: A. a target in the App `.csproj` that downloads the official standalone Tailwind exe, pinned to one version, and turns `Styles/app.css` into `wwwroot/app.css` on every build; B. the `TailwindBlazor` NuGet package (0.2.0, one author); C. install the exe by hand and run it in watch mode. Should it be A?

Recommended: A, because it uses only the official Tailwind binary.

Answer: Claude decides and proposes the exact changes. Marijn approves them.

### Added by Marijn - Blazor templates and code-behind order

Answer: A Blazor template never holds logic, only `if` statements and loops. All other logic goes in the code-behind. The code-behind is ordered logically: parameters and private fields at the top, then hooks, constructor, methods, private methods and so on. Marijn asked whether a standard order exists.

Claude: There's no official order. Microsoft doesn't prescribe one, and neither SonarQube nor the .NET IDE rules check it. The common convention is StyleCop's (SA1201 to SA1204), and for Blazor the lifecycle methods go in the order Blazor calls them.

### Q7 - Code-behind order

Question: List everything a code-behind can hold, order it logically, and apply that order to every code-behind file. The proposed order follows the flow: what comes in, what the component keeps, what the template reads, the lifecycle, what the user does, the helpers, and the clean-up.

Class declaration: attributes (`[Route]`, `[Authorize]`, `[StreamRendering]`); the primary constructor with the injected services, the base class and interfaces.

Members, top to bottom:
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

There are no `[Inject]` properties and no separate constructor, because services come in through the primary constructor. Within a group, members follow the order the template uses them.

Recommended: Yes.

Answer: Yes.

### Q5 - UI texts

Question: Should every text a user sees in the app go in a `.resx` file in the App project, never written directly in the markup or code? (The Migrator already keeps its texts in `Resources/Texts.resx`.)

Recommended: Yes.

Answer: Yes, for all code: every text shown to the user comes from a `.resx` file.

### Q8 - Languages

Question: Should the texts be English only: one `Texts.resx` per project, no translated copies, read through the generated `Texts` class (like the Migrator), without `IStringLocalizer`?

Recommended: Yes.

Answer: Yes, English.

### Q9 - Stand-ins in tests

Question: How do tests get stand-ins? A. hand-written fakes; B. NSubstitute; C. FakeItEasy. Should it be B, NSubstitute, for interfaces?

Recommended: B.

Answer: NSubstitute for mocking. Mappings are written by hand.

### Q10 - The clock

Question: Should all code read the time only through an injected `TimeProvider` (registered as `TimeProvider.System`), with `FakeTimeProvider` from `Microsoft.Extensions.TimeProvider.Testing` in tests, and `DatabaseSaveService` (which reads `DateTime.UtcNow`) added to the rewrites spec?

Recommended: Yes.

Answer: When time is tested, use that library.

### Q11 - The database in tests

Question: A. SQLite in memory; B. EF's InMemory provider; C. no database, stand-ins for an interface around the `DbContext`. Should it be A?

Recommended: A.

Answer: B, EF's InMemory provider.

### Q12 - HTTP in tests

Question: A. one small hand-written `HttpMessageHandler` stand-in per test project; B. the `RichardSzalay.MockHttp` package (MIT). Should it be A?

Recommended: A.

Answer: B, `RichardSzalay.MockHttp`.

### Q13 - Web answers in tests

Question: How does a test make the JSON text that MockHttp sends back? A. written out in the test as a raw string literal; B. a filled object (AutoFixture) turned into JSON; C. JSON files as embedded resources. Should it be A?

Recommended: A, because B uses the same settings both ways and misses a field name that differs from Sleeper's.

Marijn asked why not fill an object and turn it into JSON. Claude proposed a middle way: fill an object for most tests, plus one test per Sleeper model that reads a small hand-written JSON text, to check the field names. Renaming a property so it no longer matches Sleeper's name makes that test fail. Adding a property doesn't; the new field gets added to the JSON by hand.

Answer: Yes, the middle way.

### Q14 - Required properties

Question: Should properties that must always have a value be `required` instead of having a default (such as `= string.Empty`), with the existing models added to the rewrites spec? Then a hand-written mapper that forgets a property fails to build.

Recommended: Yes.

Answer: Yes, `required` is preferred.

### Q15 - Sealed classes

Question: Should every class that nothing inherits from be `sealed`?

Recommended: Yes, because it shows the class isn't meant to be inherited from and is a little faster.

Answer: No rule. Classes may be inherited from in the future, and it doesn't matter much. `LeagueListState` is sealed only because TimeWarp.State's README declares its example state that way (`public sealed partial class CounterState : State<CounterState>`).
-->
