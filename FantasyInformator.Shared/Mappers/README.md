# Mappers

Copies objects between two classes of models, both ways, together with their links to other models. A link that leads back to an object already copied gets that same copy, so a copy never loops forever.

Example: a `Team` with 3 `Player`s is copied into a `TeamDb` with 3 `PlayerDb`s, and each `PlayerDb` links back to that same `TeamDb`.

The examples below use the models `Team`, `TeamDb`, `Player` and `PlayerDb` in a project named `Shop`.

[[_TOC_]]

## Parts

| Part | What it does |
|------|--------------|
| `Mapper<TModel, TOtherModel>` | Base class of every mapper. It holds the 8 Map methods. A mapper only copies the fields. |
| `MapContext` | Fills the links during one copy and remembers what it already copied. Each project has its own. |
| `IMapper` | Copies an object whose class is only known while the program runs. |
| `MapperLookup` | Finds the mapper between two models in a project. |
| `MappingException` | Every error the mappers find themselves. See [Exceptions](#exceptions). |
| `FantasyInformator.Tool.MapGenerator` | A separate console app that writes `.Map()` extension methods for every model into a project, just before that project compiles. See [`.Map()` on the model](#map-on-the-model). |

## Writing a mapper

### 1. A context per project

The context says which properties are links: `IsLink` gets the class a property holds, or the class its list holds, and returns `true` for a link.

```csharp
internal sealed class ShopMapContext : MapContext
{
    protected override bool IsLink(Type type) =>
        type == typeof(Team) || type == typeof(TeamDb) || type == typeof(Player) || type == typeof(PlayerDb);
}
```

`OnCopied` is optional. It runs right after each copy is made, before its links are filled, for what every copy gets besides its own fields, such as an `Id`.

### 2. A mapper per pair of models

```csharp
internal sealed class TeamMapper : Mapper<Team, TeamDb>
{
    protected override MapContext CreateContext() => new ShopMapContext();

    protected override Team CreateCopy(TeamDb source) => new() { Name = source.Name };

    protected override TeamDb CreateCopy(Team source) => new() { Name = source.Name };
}
```

- `CreateCopy` copies only the fields, such as `Name`. Never the links, such as `Players`: the context fills those.
- The mapper needs a constructor without parameters, because the lookup makes the object itself.

## Using a mapper

### `.Map()` on the model

The console app `FantasyInformator.Tool.MapGenerator` writes `.Map()` methods for every model into a project, just before that project compiles:

```csharp
TeamDb teamDb = team.Map();
Team team = teamDb.Map();
team.Map(out SuperTeam superTeam);
```

How a project runs it, its arguments and what it writes are in the [MapGenerator README](../../Tools/FantasyInformator.Tool.MapGenerator/README.md).

### The lookup

Code that only knows the models as `TModel` and `TOtherModel` asks the lookup for the mapper:

```csharp
var mapper = MapperLookup.Find<TeamDb, Team>(typeof(TeamMapper).Assembly);
Team team = mapper.Map(teamDb);
```

- The order of the two models doesn't matter: `Find<TeamDb, Team>` and `Find<Team, TeamDb>` both work.
- The lookup searches only the project it gets. It makes one mapper object per project and order of the two models, and gives back that same object every next time.

### The 8 Map methods

Every mapper has them. Without a context, each call starts a new context. A list shares one context for all its objects.

| Method | Gives back |
|--------|------------|
| `Map(teamDb)` | `Team` |
| `Map(teamDb, context)` | `Team`, inside a copy that is already running |
| `Map(teamDbs)` | `IEnumerable<Team>` |
| `Map(teamDbs, context)` | `IEnumerable<Team>`, inside a copy that is already running |
| `Map(team)` | `TeamDb` |
| `Map(team, context)` | `TeamDb`, inside a copy that is already running |
| `Map(teams)` | `IEnumerable<TeamDb>` |
| `Map(teams, context)` | `IEnumerable<TeamDb>`, inside a copy that is already running |

## How links are copied

1. `CreateCopy` makes the copy, for example a `Team` from a `TeamDb`.
2. `OnCopied` runs.
3. The context remembers the original with its copy.
4. The context looks at every public property of the copy. `Team.Players` is a `List<Player>`, and `IsLink` is `true` for `Player`, so it is a link.
5. The original must have a property with the same name: `TeamDb.Players`.
6. The lookup finds the mapper between `PlayerDb` and `Player`, in the project of the context class.
7. Each `PlayerDb` is copied into a new list on the `Team`. A link that is `null` stays `null`.
8. A `PlayerDb` links back to its `TeamDb`. That `TeamDb` was remembered in step 3, so the same `Team` is used.

The context finds the links of each pair of classes only once and keeps them. It remembers an original only as that very object, not as another object with the same values.

## Exceptions

### `MappingException`

Every error the mappers find themselves is a `MappingException`, so a caller can catch exactly those. It holds:

| Property | Holds |
|----------|-------|
| `Message` | What went wrong, with the class names |
| `SourceType` | The class of the original, or `null` when it was not given |
| `TargetType` | The class of the copy, or `null` when the mapper doesn't know it |
| `PropertyName` | The property that caused the error, or `null` when no single property did |
| `InnerException` | The cause, when the code that throws it passes one |

It is thrown when:

| Situation | Message | `SourceType` | `TargetType` | `PropertyName` |
|-----------|---------|--------------|--------------|----------------|
| The project has no mapper between the two models | Shop has no mapper between Team and Player. | `Team` | `Player` | `null` |
| The project has more than one mapper between the two models | Shop has more than one mapper between Team and TeamDb: TeamMapper, OldTeamMapper. | `Team` | `TeamDb` | `null` |
| The copy has a link the original doesn't have | TeamDb has no property Players to map into Team. | `TeamDb` | `Team` | `Players` |
| `IMapper.Map` gets an object its mapper doesn't copy | TeamMapper does not copy a Player. | `Player` | `null` | `null` |

A missing link mapper is found during the first copy of that pair, not when the program starts. The lookup doesn't keep a failed search, so every next call throws again.

### Other exceptions

These reach the caller unchanged, not as a `MappingException`:

- An error thrown in your own `CreateCopy`, `IsLink` or `OnCopied`, such as an `ArgumentException` when a required value is empty.
- An `InvalidCastException` when, within one copy, the same original object is copied into two different classes.
- A `MissingMethodException` from the lookup when a mapper has no constructor without parameters.
