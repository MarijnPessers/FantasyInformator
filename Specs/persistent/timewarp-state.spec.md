# TimeWarp.State

Everything known about TimeWarp.State and its mediator, TimeWarp.Mediator. Add every new fact here, with its source. A fact without "Verified" is unverified.

Collected up to 15 Nov 2026.

## Version in use

- The solution uses `TimeWarp.State` 12.0.0-beta.8 (State, State.Tests and App.Tests). Verified in the `.csproj` files.
- 11.x depends on MediatR, which is banned. Version 12 uses TimeWarp.Mediator instead.
- 12.0.0-beta.8 depends on `TimeWarp.Mediator.Contracts` 14.0.0-beta.1, `AnyClone` 1.1.6, `JetBrains.Annotations`, `Microsoft.AspNetCore.Components.Web` 10.0.12, `Microsoft.CodeAnalysis.CSharp` 4.14.0, `Microsoft.Extensions.Logging.Abstractions` 10.0.12 and `TypeSupport` 1.2.0. Verified in `FantasyInformator.State/obj/project.assets.json`.
- The package holds `lib/net10.0/TimeWarp.State.dll`, the analyzers `timewarp-state-analyzer.dll` and `timewarp-state-source-generator.dll`, and `build`, `buildTransitive` and `buildMultiTargeting` copies of `TimeWarp.State.props`. Verified in `project.assets.json`.
- The package also holds `contentFiles/any/net10.0/tsconfig.json`, which NuGet links into every project that references it, so it shows in Solution Explorer. We reference the package with `ExcludeAssets="contentFiles"` to stop that. Verified: the file is in the package folder and Marijn saw it in the IDE, 16 Nov 2026.
- An earlier session noted 12.0.0-beta.17 (6 Nov 2026) as the newest beta and said it targets `net11.0` only. Not rechecked.

## Where to look

- Source at the version in use: https://github.com/TimeWarpEngineering/timewarp-state/tree/v12.0.0-beta.8. The tag exists, but `README.md` at the tag gives a 404.
- The README of the version in use: https://www.nuget.org/packages/TimeWarp.State/12.0.0-beta.8
- The docs site (https://timewarpengineering.github.io/timewarp-state/) is out of date: it still describes MediatR and .NET 8.
- `master` is newer than beta.8 and differs from it. For example, `UseReduxDevTools` was removed in 12.0.0-beta.11, State 12.0.0-beta.16 moved to TimeWarp.Mediator 15.0.0-beta.1, and Mediator 15.0 isn't a drop-in for 14.0. Always read the `v12.0.0-beta.8` tag.
- TimeWarp's own examples at the tag: `samples/00-state-action-handler/server/sample-00-server/` (`program.cs`, `mediator-scope.cs`, `global-usings.cs`, `.csproj`) and `tests/timewarp-state-tests/`.

## Registering

Source for all of this: the beta.8 sample `program.cs`, `mediator-scope.cs` and `.csproj`, `source/timewarp-state/timewarp-state.csproj`, `source/timewarp-state/assembly-marker.cs`, `source/timewarp-state/extensions/service-collection-extensions.add-timewarp-state.cs` and `tests/test-app/test-app-server/` at the tag.

- `AddTimeWarpState` registers nothing mediator-related. Its code says: "Nothing mediator-related is registered here."
- The project that starts the app (the host) registers the mediator itself:
  - It references `TimeWarp.Mediator.Generators` 14.0.0-beta.1 with `PrivateAssets="all"`. That source generator writes `AddGeneratedMediator()`, `AddGeneratedMediator<TScope>()` and a `Mediator` class into the host assembly.
  - `TimeWarp.State` itself must not reference the generator. `TimeWarp.State` alone is not enough.
  - The sample registers it like this:

    ```csharp
    builder.Services.AddGeneratedMediator<ClientPipeline>();
    builder.Services.AddTimeWarpState();
    ```

  - The host holds `[assembly: MediatorScope(typeof(ClientPipeline))]`. Without it, a client `Send` gives the analyzer error TWM004.
- Only one generated mediator may exist in a compile graph. TimeWarp's test app uses `AddServerPipelineMediator()` for its second one, to avoid CS0121.
- The generated DI methods are in the namespace `Microsoft.Extensions.DependencyInjection`. Source: a comment in `test-app-server.csproj`.
- `GeneratedMediatorServiceCollectionExtensions`, which holds `AddGeneratedMediator<TScope>()`, is `internal`, like every generated type, so only code in the project that references the generator can call it. A project that references only that project can't. Source: `source/timewarp-mediator-generators/mediator-emitter.cs` and `documentation/m1-generated-mediator.md` at the Mediator tag `v14.0.0`, read 25 Nov 2026.
- The documented layout is: the host references `TimeWarp.Mediator.Generators` and calls `AddGeneratedMediator<TScope>()`; a library that holds handlers references only Contracts and Analyzers and adds `[assembly: MediatorAssembly]`. Source: `documentation/generated-vs-legacy.md` at `v14.0.0`.
- Not stated in the docs read: whether the generator works in a library project that isn't the host, whether it only works for an `Exe`, and what happens when two projects of one graph reference the generator (the docs only say the internal types avoid CS0436). `message-graph-builder.cs` and `membership.cs` at `v14.0.0` gave a 404.
- TimeWarp.State's own `assembly-marker.cs` holds `[assembly: MediatorAssembly]`, `[assembly: MediatorScope(typeof(ClientPipeline))]` and four `[assembly: MediatorBehavior(...)]` lines.
- `AddTimeWarpState(x => x.Assemblies = [...])` exists in beta.8. Verified: `LeagueListStateTests` compiles with it. In 11.0.3 it was needed, because by default only the calling assembly was scanned.
- The store depends on `ISender<ClientPipeline>`. TimeWarp's `store-get-or-add-tests.cs` registers a stand-in `ISender<ClientPipeline>`.
- Verified on 15 Nov 2026: `AddTimeWarpState` alone leaves the plain `TimeWarp.Mediator.ISender` unregistered ("No service for type 'TimeWarp.Mediator.ISender' has been registered.").
- Our setup, untested:
  - `FantasyInformator.State.Tests` references the generator, holds `MediatorScope.cs`, calls `AddGeneratedMediator<ClientPipeline>()` before `AddTimeWarpState`, and asks for `ISender<ClientPipeline>`.
  - `FantasyInformator.State` holds `MediatorAssembly.cs` with `[assembly: MediatorAssembly]` and `[assembly: MediatorScope(typeof(ClientPipeline))]`, so its handlers are found.

## Pipelines

- `ClientPipeline` is in `namespace TimeWarp.State` (`public sealed class ClientPipeline;`). There is also a `ServerPipeline`. Both are marker types. Verified in `source/timewarp-state/pipelines/` at the tag.
- `client-pipeline.cs` says that server handlers never run the client pipeline's behaviors.

## States, actions and handlers

Verified: our code compiles with these.

- A state: `public sealed partial class LeagueListState : State<LeagueListState>`, with `public override void Initialize()`.
- An action: a `record` that implements `IAction`, nested in its state class.
- A handler: a class that derives from `ActionHandler<TAction>` and overrides `public ValueTask Handle(TAction action, CancellationToken cancellationToken)`.
- `IStore.GetState<T>()` reads a state outside a component.
- `ActionHandler<TAction>` is in TimeWarp.Mediator.Contracts, in the namespace `TimeWarp.Mediator`, not in TimeWarp.State. Source: `source/timewarp-mediator-contracts/i-action-handler.cs` at the Mediator tag `v14.0.0-beta.1`:

  ```csharp
  public interface IActionHandler<in TAction> : IRequestHandler<TAction>
      where TAction : IAction
  {
      new ValueTask Handle(TAction request, CancellationToken cancellationToken);
  }

  public abstract class ActionHandler<TAction> : IActionHandler<TAction>
      where TAction : IAction
  {
      public abstract ValueTask Handle(TAction request, CancellationToken cancellationToken);

      Task IRequestHandler<TAction>.Handle(TAction request, CancellationToken cancellationToken)
      {
          return Handle(request, cancellationToken).AsTask();
      }
  }
  ```

- TimeWarp.State adds `StateActionHandler<TAction>` (namespace `TimeWarp.State`, `source/timewarp-state/base/state-action-handler.cs`), which derives from `TimeWarp.Mediator.ActionHandler<TAction>`. TimeWarp's sample handlers derive from it. We derive from `ActionHandler<TAction>` directly.
- There's no global `using TimeWarp.State;`. Verified on 15 Nov 2026: in State.Tests, `ClientPipeline` gave CS0246 in a file without `using TimeWarp.State;`.
- TimeWarp's own test and sample projects put `global using TimeWarp.Mediator;` and `global using TimeWarp.State;` in a `global-usings.cs`.

## How we implement states, actions and handlers

This is how it must be done. Source: Marijn's code in `FantasyInformator.State`, 15 Nov 2026.

- Every state class is a `partial` class. Its actions and handlers are nested inside it, in other files that are part of the same partial class.
- `Store/` holds the states, one file per state: `Store/LeagueState.cs`.
  - `public sealed partial class LeagueState : State<LeagueState>`, with its data as properties with a `private set` and `public override void Initialize()`.
- `Store/<state name without "State">/` holds that state's actions and handlers, one file per action and its handler, named after the handler: `Store/League/AddLeagueHandler.cs`.
  - The file holds `public partial class LeagueState { ... }` with the action and the handler nested inside.
  - The action: `public record AddLeagueAction(League League) : IAction;`
  - The handler: `public class AddLeagueHandler(IStore store) : ActionHandler<AddLeagueAction>`. It reads its state through a private property, `store.GetState<LeagueState>()`, and overrides `public override ValueTask Handle(AddLeagueAction request, CancellationToken cancellationToken)`.
- Every file in `Store/` and its subfolders uses the namespace `FantasyInformator.State.Store`, also in a subfolder, because the parts of a partial class must share one namespace. `.editorconfig` turns IDE0130 (namespace must match folder) off.
- Abstract base classes go in `Abstractions/`, namespace `FantasyInformator.State.Abstractions`.
- Behaviors go in `Behaviors/`, namespace `FantasyInformator.State.Behaviors`: `Behaviors/TraceBehavior.cs`. They're declared in `MediatorAssembly.cs` with `[assembly: MediatorBehavior(..., Scope = typeof(ClientPipeline))]`.

## How the generator finds handlers

Source: `source/timewarp-mediator-analyzers/membership.cs`, `message-graph-builder.cs`, `source/timewarp-mediator-contracts/mediator-assembly-attribute.cs`, `mediator-assemblies-attribute.cs` and `source/timewarp-mediator-generators/build-transitive/timewarp-mediator-generators.props`, at the Mediator tag `v14.0.0-beta.1` (commit `40a9841`).

- The generator only looks for handlers in "member" assemblies. An assembly without membership is ignored without any error, which leads to `NoHandlerException` when the action is sent. Verified on 15 Nov 2026: "No handler registered for request type 'FantasyInformator.State.LeagueListState+AddLeagueAction'."
- A project that references `TimeWarp.Mediator.Generators` is a member through the package's props (`TimeWarpMediatorAssembly=true`).
- A referenced project is a member only when it holds `[assembly: MediatorAssembly]`, or when the host lists it with `[assembly: MediatorAssemblies(typeof(SomeTypeInIt))]`. Both attributes are in the namespace `TimeWarp.Mediator`. Referencing only TimeWarp.State (and so Contracts) isn't enough.
- TimeWarp.State itself holds `[assembly: MediatorAssembly]` and `[assembly: MediatorScope(typeof(ClientPipeline))]`, and must not reference the generator.
- A handler must be a concrete, non-generic class that isn't private or protected (`internal` is fine). The generator reads all of its interfaces, including those of its base classes, so an abstract base class of our own in between is fine.
- The scope (pipeline) of a handler comes only from the handler's and the action's own types and their own assembly. The host's `MediatorScope` doesn't apply to handlers in another project. Not known: whether a handler without a scope can be reached through `ISender<ClientPipeline>`.
- More documentation, not read yet: `documentation/generated-vs-legacy.md`, `documentation/m1-generated-mediator.md`, `documentation/m2-named-pipelines.md` and `migration.md` in the Mediator repository.

## Behaviors (steps around every action)

- `assembly-marker.cs` in TimeWarp.State registers its behaviors like this: `[assembly: MediatorBehavior(typeof(ReduxDevToolsBehavior<,>), order: 100, Scope = typeof(ClientPipeline))]`.
- At the Mediator tag `v14.0.0-beta.1`, the interface is `IPipelineBehavior<in TRequest, TResponse> where TRequest : notnull`, with `Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)`. `next` is `public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken t = default);`. Namespace `TimeWarp.Mediator`. Source: `source/timewarp-mediator-contracts/i-pipeline-behavior.cs` at that tag. (`master` has a different, `ValueTask`-based signature.)
- Behaviors are woven in at build time with `[assembly: MediatorBehavior]`, not registered through DI.
- `MediatorBehaviorAttribute(Type behaviorType, int order = 0)` with a settable `Type? Scope`. A lower `Order` wraps the rest (runs first); on equal order, the first attribute is outermost. A behavior without `Scope` runs only on the unscoped pipeline, never on a scoped one. Source: `source/timewarp-mediator-contracts/mediator-behavior-attribute.cs` at the Mediator tag `v14.0.0-beta.1`.
- From the generator's code (`message-graph-builder.cs`, `mediator-emitter.cs` at that tag), not tested:
  - Behaviors are read from every member assembly and apply to every request in the graph with the same scope, also to handlers in the assembly that declares them.
  - An `IAction` gets `Unit` as its response, so a behavior `IPipelineBehavior<TRequest, TResponse>` (or one with `where TRequest : IAction`) wraps actions too.
  - The `Aot` profile drops all behaviors.
- The host's generated `AddGeneratedMediator<TScope>()` registers every behavior of that scope in DI as scoped, and the generated mediator gets each one with `GetRequiredService`. So a behavior needs no registration by hand, and gets its own services through its constructor. `AddTimeWarpState` registers no behaviors. Source: `mediator-emitter.cs` (`EmitHandlerRegistrations`, `EmitDispatchMethods`) at the Mediator tag `v14.0.0-beta.1`, and `service-collection-extensions.add-timewarp-state.cs` at the State tag `v12.0.0-beta.8`.
- How TimeWarp's own packages switch on a behavior, at the State tag `v12.0.0-beta.8`: always with `[assembly: MediatorBehavior(...)]`. No package registers a behavior through DI or an options class.
  - `TimeWarp.State.Telemetry` switches its `TelemetryBehavior` on itself, in `source/timewarp-state-telemetry/assembly-marker.cs`: `[assembly: MediatorAssembly]`, `[assembly: MediatorScope(typeof(ClientPipeline))]` and `[assembly: MediatorBehavior(typeof(TelemetryBehavior<,>), order: 350, Scope = typeof(ClientPipeline))]`.
  - `TimeWarp.State.Plus` leaves its behaviors off. The host declares the ones it wants, such as in `tests/test-app/test-app-client/mediator-behaviors.cs`: `[assembly: MediatorBehavior(typeof(PersistentStatePostProcessor<,>), order: 520, Scope = typeof(ClientPipeline))]`.
  - Each library keeps its `[assembly: ...]` lines in `assembly-marker.cs` at the project root.
  - Their behaviors look like ours: `sealed class TelemetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IAction`, with `Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)`.
- `StateTransactionBehavior` (order 300) copies the state before each action and puts the copy back when the handler throws. `TimeWarpStateOptions.UseStateTransactionBehavior` (`bool`, default `true`) switches it off: it then goes straight to `next`, without copying. Source: `source/timewarp-state/extensions/timewarp-state-options.cs` and `source/timewarp-state/features/pipeline/state-transaction-behavior.cs` at the State tag `v12.0.0-beta.8`.
- The copy is made with AnyClone 1.1.6. AnyClone keeps a `static` `Dictionary` (`_memberUpdaterCache` in `CloneProvider.cs`) without a lock, filled the first time it copies an `init`-only field. Two copies at once can break it: "Operations that change non-concurrent collections must have exclusive access". Verified on 16 Nov 2026 in State.Tests running in parallel. Source: AnyClone `master`; GitHub has no tag for 1.1.6.
- So the State tests switch `UseStateTransactionBehavior` off. The app still has the risk when two actions start at once before each type was copied for the first time.
- `TimeWarp.Mediator.Unit` is the answer of an action. It has `Unit.Value` and `Unit.Task` (a finished `Task<Unit>`). Source: `source/timewarp-mediator-contracts/unit.cs` at the Mediator tag `v14.0.0-beta.1`.
- Built-in, with their order:
  - `StateTransactionBehavior` (300), in TimeWarp.State
  - `TelemetryBehavior` (350), in the opt-in package `TimeWarp.State.Telemetry`
  - `RenderSubscriptionsPostProcessor` (400), only when the host references `TimeWarp.State.Blazor`

## Analyzers

Source: `source/timewarp-state-analyzer/` at the State tag `v12.0.0-beta.8`, read 22 Nov 2026.

- TWS0001 (Error, `timewarp-state-action-analyzer.cs`): every non-abstract class, record or struct that implements `TimeWarp.Mediator.IAction` must be nested, at any depth, in a type that implements `TimeWarp.State.IState`. `State<T>` isn't required, `partial` isn't required. No exemption for test assemblies, private types or attributes; only generated code is skipped. Verified: a record nested in a test class gave TWS0001, 22 Nov 2026.
- `StateInheritanceAnalyzer`: error when a class derives from `State<T>` with a `T` other than itself; warning when such a class isn't `sealed` or `abstract`.
- `StateReadOnlyPublicPropertiesAnalyzer`: error on a public property of a state whose setter isn't private.
- `StateImplementationAnalyzer` (TWS001): a class that derives directly from `State<T>` needs a parameterless constructor or `ICloneable`.
- TWS0002–TWS0008 (`HandlerMustNotSendActionAnalyzer`, `CatalogActionAnalyzer`): not read.

## Tracing

- Core TimeWarp.State and TimeWarp.Mediator create no `Activity`, as far as checked (folder lists and READMEs only, not every file).
- `TimeWarp.State.Telemetry` (in the beta.8 tag) makes each action a span:
  - `ActivitySource` named `"TimeWarp.State"` (`TimeWarpStateTelemetry.ActivitySourceName`).
  - The span is named after the action's nested type name, tagged with the action type, and marked `Ok` or `Error`.
  - It only starts a span when something listens (`ActivitySource.HasListeners()`).
  - Setup from its README: `tracing.AddSource(TimeWarpStateTelemetry.ActivitySourceName);`
  - Not checked: whether it's published on NuGet for 12.0.0-beta.8.
- We don't use it. Our own behavior, `TraceBehavior`, starts the activity itself.

## Blazor

- The Blazor parts are in a separate package, `TimeWarp.State.Blazor`. A Blazor host calls `AddTimeWarpStateBlazor()` besides `AddTimeWarpState()`. Source: an earlier session, not rechecked at beta.8.
- The store is registered as scoped: `serviceCollection.TryAddScoped<IStore, Store>();`. In Blazor Server a scope lives as long as one circuit (an ASP.NET Core fact, not checked in TimeWarp), so each user session has its own store and its own states. Data loaded in a scope made at app start never reaches the store of a circuit. Source: `source/timewarp-state/extensions/service-collection-extensions.add-timewarp-state.cs` at the State tag `v12.0.0-beta.8`, read 25 Nov 2026.
- A state class is registered as transient (`TryAddTransient(type)`, in `EnsureStates`). `Store.GetState(Type)` keeps one instance per state type in its own `ConcurrentDictionary` and creates it with `ServiceProvider.GetRequiredService(stateType)`, so a state class can have constructor injection. It then sets `Sender` and calls `Initialize()`. Source: `source/timewarp-state/store/store.cs` at the same tag, read 25 Nov 2026.
- Not known: the name of the component base class (maybe `TimeWarpStateComponent`), and whether the Blazor connection already has a running `Activity`.

## Other packages and features

- `TimeWarp.State.Plus`: extras such as action tracking. Not opened.
- Action catalog (beta.8 README): opt-in. The source generator writes a registry per assembly, registered with `builder.Services.AddActionCatalog(typeof(AssemblyMarker).Assembly);`.
- The only change in beta.8 (PR #615): "allow-list JavaScript-dispatchable actions".
