Database refactor: generic services

# Handoff: Database project refactor

Rebuilt from the session of 2026-11-20 (14:02–16:35 UTC). That session hit the weekly limit before its handoff was written.

## Scope

- Only the Database project and its tests are edited. Files in other projects only when Marijn names them.
- Database knows nothing about State. Do not touch State, even when it breaks.
- Marijn builds and runs everything. Nothing in this session was built.

## Decisions (take as truth, do not discuss)

- **Database model names:** every database model ends in `Db` (`PlayerDb`, `TrackedModelDb`). Table names come from the `DbSet` property names, so no `[Table]` attribute. Old migrations keep their old names.
- **Namespaces:** plural where a plural exists (`FantasyTeams`, `Leagues`, `Lineups`, `Players`, `Seeds`), and always the same as the folder. `Administration`, `Nfl`, `Scoring`, `Tracking` stay. No `AppX = …` or `DatabaseX = …` aliases.
- **Seed ids:** `SeedIds.For(nameof(SlotDb), …)`; `SeedIds.For` strips the `Db` ending, so the ids stay the same. No magic strings. `FantasyInformator.Database/Context/Seeds/SeedIds.cs`
- **No `TrackedModelMapper`.** Marijn deleted it. Never recreate it, and never set `Id` or `State` in each mapper.
- **Id:** copied by the generic services, not the mappers. Reading: `FantasyInformator.Database/Services/DatabaseReadService.cs`. Saving: `MapWithId` in `FantasyInformator.Database/Services/DatabaseSaveService.cs`. `MappedAssert` skips `Id`.
- **State:** a new model starts as `New`. The read service wraps its mapping in `TrackedModel.SetNewDefaultState(TrackingState.Unchanged)`, so models read from the database start as `Unchanged`. `FantasyInformator.Domain/Tracking/TrackedModel.cs`
- **Lists:** every list in Domain and in the database models is `List<X>?` with no default. Null means "not retrieved", empty means "retrieved, none found". Not retrieving is the default.
- **Mappers:** every mapper is an extension method named `Map()`. Each mapper also has `Map()` on `IEnumerable<X>`, in both directions. The project tells the context. Example: `FantasyInformator.Database/Mappers/Players/PlayerMapper.cs`.
- **Generic save:** `SaveAsync<TModel, TDatabaseModel>(models, map, cancellationToken)`; the caller passes `x => x.Map()`.
- **Generic read:** Logic only ever calls `ReadAll<Player>()`-style methods. `Player` always gives `PlayerDb`, no exceptions for now. It reads all rows of `PlayerDb` without links, then calls the `Map()` on `IEnumerable<PlayerDb>`. The built call per model is cached in a dictionary. Heavy plain-word comments on this reflection code are wanted.
- **Generic services are internal:** `IDatabaseReadService`, `DatabaseReadService`, `IDatabaseSaveService`, `DatabaseSaveService`.
- **One public service per object:** `IPlayerService : IEntityService<Player>` with `PlayerService : EntityService<Player>`. `EntityService<TModel>` has a ready `virtual GetAll`; a service overrides it or adds methods (player positions, saving) only when needed. `FantasyInformator.Database/Services/EntityService.cs`, `FantasyInformator.Database/Services/Players/PlayerService.cs`
- **Removed as obsolete:** `ReadServiceBase`, `LeagueReadService`, `FantasyTeamReadService` and their registrations.
- README bullets already added this session: the `Unchanged` state (Database README) and the `New` state (Sleeper and Nflverse READMEs). Domain README group headings are plural.

## Open work

1. Rewrite the 16 other Database mappers like `PlayerMapper.cs`: drop the `TrackedModelMapper.MapToAppModel(...)` / `TrackedModelMapper.Map(...)` wrapper (it no longer exists, so Database doesn't compile), and map lists null-safe with `?.Select(x => x.Map()).ToList()` instead of `[.. x.Select(...)]`.
2. Add the two `IEnumerable` `Map()` overloads to those 16 mappers. `FindRowMapper` in `DatabaseReadService.cs` needs exactly one public static `Map` per `IEnumerable<XDb>` → `IEnumerable<X>`.
3. Ask Marijn before doing the same for the mappers outside Database (Sleeper, Nflverse). He asked for it once, then limited the scope to Database.
4. `FantasyInformator.Database.Tests/Mappers/Tracking/TrackedModelMapperTests.cs` tests the deleted class. Ask Marijn: delete it or replace it.
5. `MarkUnchanged()` in `FantasyInformator.Domain/Tracking/TrackedModel.cs` has no callers since the scope replaced it. Ask Marijn whether to remove it.
6. `using System.Diagnostics.Metrics;` in `FantasyInformator.Domain/Tracking/TrackedModel.cs` is unused.
7. ⚠️ Two `IPlayerService` interfaces exist: `FantasyInformator.Database/Services/Players/PlayerService.cs` and `FantasyInformator.Sleeper/Services/Interfaces/IPlayerService.cs`. Code that imports both namespaces gets an ambiguous name. Tell Marijn; he decides.
8. Write tests for the new code: `DatabaseReadService.ReadAll`, `EntityService.GetAll`, `MapWithId` in `DatabaseSaveService`, `TrackedModel.SetNewDefaultState`. Tests use stand-ins, never a real database. The test project has no substitute or in-memory database package yet.
9. Propose a Database README update for: the generic read, the `X` → `XDb` rule, internal generic services, one service per object, null versus empty lists, and the id being copied by the services.
10. `models.drawio.svg` still shows the old singular group names. Follow `specs/persistent/readme-images.spec.md`.
11. Known broken outside scope, leave alone: four State files still use `FantasyInformator.Database.Models.Leagues` and `League` (`FantasyInformator.State/Store/LeagueState.cs`, `FantasyInformator.State/Store/League/AddLeagueHandler.cs`, `FantasyInformator.State.Tests/LeagueListStateTests.cs`, `FantasyInformator.State.Tests/Behaviors/TraceBehaviorTests.cs`).
12. For Marijn: after the build is green, `dotnet ef migrations add` should give an empty migration (snapshot only), proving the `Db` rename changed no tables.

## Related

- `specs/WIP/WIP_1.spec.md` holds the player fetch work (`FantasyInformator.Logic/Services/Players/PlayerLogic.cs` still throws `NotImplementedException`). This refactor started from its item 1.

## Suggested skills

- `mattpocock-skills:codebase-design`: for the shape of the generic services and the per-object services.
- `mattpocock-skills:tdd`: for item 8.
- `code-review`: after items 1–2, to review the Database changes.
