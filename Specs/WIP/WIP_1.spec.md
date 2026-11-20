playerfetch testing implement

# Handoff

Focus of the next session: build what the flow tests of the players fetch need, then write those tests. That's step 2 in `specs/fleeting/steps.spec.md`.

## Read first

- `CLAUDE.local.md`: follow every Communication rule strictly. When Marijn types "reminder", redo the last answer by those rules.
- `specs/index.spec.md`, then:
  - `specs/fleeting/players-update.spec.md`: the Flow and Tests sections are the source for this work.
  - `specs/persistent/conventions.spec.md`: errors, progress reports, tracing, retries, tests.
  - `specs/persistent/timewarp-state.spec.md`: the section "How we implement states, actions and handlers", and the Behaviors section.
  - `FantasyInformator.Database/README.md`: the scenarios the Database tests follow.

## Where things stand

The agent built none of this and ran no tests. Marijn builds and runs them.

- Tracing: `FantasyInformator.State/Behaviors/TraceBehavior.cs` runs around every action, declared in `FantasyInformator.State/MediatorAssembly.cs`. Its label comes from `FantasyInformator.State/Behaviors/ActionMarker.cs`, with a new GUID each time the app starts. `TracedActionHandler` is gone.
- Players in State: `FantasyInformator.State/Store/PlayerState.cs`, `Store/Player/FetchAllPlayersHandler.cs` and `Store/Player/LoadAllPlayersHandler.cs`. `LoadAllPlayersAction(bool Force = false)` skips Logic when the state already holds players.
- Logic: `FantasyInformator.Logic/Services/Player/PlayerLogic.cs` holds `IPlayerLogic`. Both methods throw `NotImplementedException`.
- Tests: `FantasyInformator.State.Tests/Store/Player/` and `FantasyInformator.State.Tests/Behaviors/TraceBehaviorTests.cs`. The player tests switch off `UseStateTransactionBehavior`, because of a race in AnyClone (see the Behaviors section of the TimeWarp spec). ⚠️ Marijn hasn't yet reported whether they pass with that fix.

## Items

Only what the flow tests need. Sleeper and Database stay stand-ins (NSubstitute), so their real code and the snackbar aren't needed.

1. Next step: Database tests, one test per scenario in `FantasyInformator.Database/README.md`. Unit tests with stand-ins (NSubstitute) and EF's InMemory provider.
2. Database interfaces for the players: read the last fetch (falling back to a waiting file), save the players together with the `LastSleeperCall` time, and read all players.
3. Build `FetchAllPlayersAsync` and `LoadAllPlayersAsync` in `PlayerLogic`, as the Flow section of `players-update.spec.md` describes.
4. The progress state in State and its `IProgress<ProgressReport>` class, as the Progress reports part of `conventions.spec.md` describes.
5. The flow tests in `FantasyInformator.Tests`, one per scenario in `players-update.spec.md`.

## Open with Marijn

- ❓ `FantasyInformator.State/Store/LeagueState.cs` uses the Database `League` model, while `conventions.spec.md` says only Database knows its own models. Not answered yet.
- ❓ The proposed rule "In a code walkthrough, skip `using` lines." (category Communication) wasn't answered. Marijn said: never explain a `using`.
- The app still carries the AnyClone risk when two actions start at once. No decision yet.

## How Marijn wants to work

- Yes/no questions: answer with only "yes" or "no".
- Explanations: basic words, short, exactly what was asked. Marijn says "tmi" when it's too much.
- Walkthroughs: one line per message, as `path:line` only. Never explain a `using`.
- Never ask for "implement" while Marijn is asking questions to understand something.
- The memory folder is outside the solution folder, so don't write to it.

## Suggested skills

- `mattpocock-skills:tdd` for items 1 and 5, and for building items 3 and 4 test-first.
