# FantasyInformator.Tool.MapGenerator

A console app that writes `.Map()` extension methods for every model into a project, just before that project compiles. With them, code writes `player.Map()` instead of first making a `PlayerMapper` and calling `mapper.Map(player)`.

It reads the models from the built DLL of another project, such as `FantasyInformator.Domain.dll`, through reflection. It never needs the mappers themselves, so it works before the project with the mappers is built. It references no other project. The code it writes uses `Mapper` and `MapperLookup` from `FantasyInformator.Shared` (see the [Mappers README](../../FantasyInformator.Shared/Mappers/README.md)).

This README uses the real values of `FantasyInformator.Database`, the only project that runs it now.

[[_TOC_]]

## How a project runs it

These are the parts of `FantasyInformator.Database.csproj` that run the tool, exactly as they are:

```xml
<PropertyGroup>
  <DomainProject>..\FantasyInformator.Domain\FantasyInformator.Domain.csproj</DomainProject>
  <MapGeneratorProject>..\Tools\FantasyInformator.Tool.MapGenerator\FantasyInformator.Tool.MapGenerator.csproj</MapGeneratorProject>
  <MapGeneratorOutputFolder>$(MSBuildProjectDirectory)\Generated</MapGeneratorOutputFolder>
</PropertyGroup>

<ItemGroup>
  <ProjectReference Include="$(DomainProject)" />
  <ProjectReference Include="..\FantasyInformator.Shared\FantasyInformator.Shared.csproj" />
  <ProjectReference Include="$(MapGeneratorProject)" ReferenceOutputAssembly="false" />
</ItemGroup>

<ItemGroup>
  <Compile Remove="Generated\**" />
</ItemGroup>

<Target Name="WriteMapMethods" BeforeTargets="CoreCompile" DependsOnTargets="ResolveProjectReferences">
  <MSBuild Projects="$(MapGeneratorProject)" Targets="GetTargetPath">
    <Output TaskParameter="TargetOutputs" PropertyName="MapGeneratorAssembly" />
  </MSBuild>
  <MSBuild Projects="$(DomainProject)" Targets="GetTargetPath">
    <Output TaskParameter="TargetOutputs" PropertyName="DomainAssembly" />
  </MSBuild>
  <Exec Condition="Exists('$(MapGeneratorAssembly)') And Exists('$(DomainAssembly)')"
        LogStandardErrorAsError="true"
        Command="dotnet &quot;$(MapGeneratorAssembly)&quot; --models &quot;$(DomainAssembly)&quot; --model-base FantasyInformator.Domain.BaseEntity --suffix Db --other-models-namespace FantasyInformator.Database.Models --extensions-namespace $(RootNamespace) --output &quot;$(MapGeneratorOutputFolder)&quot;" />
  <ItemGroup>
    <Compile Include="Generated\**\*.cs" />
  </ItemGroup>
</Target>
```

What happens when Database builds:

1. The `ProjectReference`s make Domain, Shared and the tool build first. `ReferenceOutputAssembly="false"` keeps the tool out of Database itself: it only has to be built.
2. Right before `CoreCompile`, the step where the compiler runs, the target `WriteMapMethods` starts.
3. The two `MSBuild` calls ask the tool project and the Domain project for the path of their built DLL, such as `Tools\FantasyInformator.Tool.MapGenerator\bin\Debug\net11.0\FantasyInformator.Tool.MapGenerator.dll`.
4. `Exec` runs the tool with `dotnet`. It only runs when both DLLs exist, so a Visual Studio background build before the first real build doesn't fail. `LogStandardErrorAsError` shows the tool's error message as a build error.
5. The tool writes the files into `FantasyInformator.Database\Generated`.
6. `Generated\**` was left out of the normal file list (`Compile Remove`) and is added now (`Compile Include`), so the compiler sees exactly the files of this run, also the ones that were just written or deleted.
7. The compiler builds Database, with the written files.

## Arguments

Each argument is a name followed by its value.

| Argument | Holds | Database gives |
|----------|-------|----------------|
| `--models` | The path of the DLL with the models. Required. | The built `FantasyInformator.Domain.dll` |
| `--model-base` | The full name of the class every model derives from. Required. | `FantasyInformator.Domain.BaseEntity` |
| `--prefix` | Text before the model name. Optional. | (none) |
| `--suffix` | Text after the model name. Optional. | `Db` |
| `--other-models-namespace` | The namespace that takes the place of the DLL name in the namespace of a model. Required. | `FantasyInformator.Database.Models` |
| `--extensions-namespace` | The namespace of the written classes. Required. | `FantasyInformator.Database` (the `RootNamespace`) |
| `--output` | The folder the files are written to. Required. | `FantasyInformator.Database\Generated` |
| `--pair` | `Model=OtherModel`, both as full names, for a model that doesn't follow the naming rule. May be given more than once. | (none) |

## What it writes

### The models it finds

Every class in the DLL that derives from `--model-base`. A class that is abstract, generic or declared inside another class is skipped. In Domain these are the 17 classes that derive from `BaseEntity`, such as `Player`, `League` and `Season`.

### The other model of each model

The name of the model with `--prefix` before it and `--suffix` after it, in the namespace of the model with the DLL name swapped for `--other-models-namespace`:

| Model | Other model |
|-------|-------------|
| `FantasyInformator.Domain.Players.Player` | `FantasyInformator.Database.Models.Players.PlayerDb` |
| `FantasyInformator.Domain.Nfl.Season` | `FantasyInformator.Database.Models.Nfl.SeasonDb` |

The tool doesn't check that the other model exists. It can't, because Database isn't built yet.

### One file per class with a `.Map()`

For `Player` it writes `PlayerMapExtensions.g.cs` and `PlayerDbMapExtensions.g.cs`. Each holds a class with two methods: one for a single object and one for a list.

```csharp
PlayerDb playerDb = player.Map();
Player player = playerDb.Map();
IEnumerable<PlayerDb> playerDbs = players.Map();
IEnumerable<Player> players = playerDbs.Map();
```

The classes are `internal`, in the namespace `FantasyInformator.Database`, so all code in Database finds them without a `using`, and no other project sees them. Each method asks `MapperLookup` for the mapper between the two classes, in the project the written class is in, and calls its `Map`.

### One file with the Map method through out

`MapExtensions.g.cs` holds one generic method for a copy into any class that has a mapper in the project, also a class the tool wrote no `.Map()` for:

```csharp
player.Map(out SuperPlayer superPlayer);
```

C# can't read the class of the copy from `var superPlayer = player.Map()`, and two `Map()` methods can't differ only in what they return. So the class of the copy is named through `out`.

### A pair that doesn't follow the naming rule

With `--pair FantasyInformator.Domain.Players.Player=FantasyInformator.Database.Models.Players.SuperPlayer`:

- `player.Map()` gives a `SuperPlayer`.
- `superPlayer.Map()` gives a `Player`.
- `playerDb.Map()` still gives a `Player`.

A pair goes before the naming rule. A class can be in only one `--pair`.

## The Generated folder

- Each run, the tool deletes every file in `Generated` that it didn't write this time, such as the file of a model that no longer exists.
- It writes a file only when its content changed. An unchanged file keeps its date, so the compiler doesn't run again for nothing.
- Only the tool writes in this folder. A change made by hand is lost on the next build.
- Until the first build, `Generated` is empty, so Visual Studio underlines `player.Map()` in red. After the first build, the red goes away.

## When it fails

The build stops with the tool's message when:

- an argument is unknown, missing, given twice or without a value
- a `--pair` isn't written as `Model=OtherModel`, or a class is in two `--pair`s
- the class of `--model-base` isn't in the DLL
- a model's namespace doesn't start with the DLL name, such as a model in `Shop.Players` inside `FantasyInformator.Domain.dll`
- two classes with a `.Map()` have the same name, such as `Domain.Players.Player` and `Domain.Archive.Player`, because both would get `PlayerMapExtensions`

Later failures:

- When the other model doesn't exist, such as a model without a `Db` class, the compiler fails on the written file.
- When the mapper doesn't exist, the first `.Map()` call throws a `MappingException`.

## The code

| Class | What it does |
|-------|--------------|
| `Program` | Runs the steps below and writes the message of a `MapGeneratorException` to the error output, with exit code 1. |
| `MapGeneratorArguments` | Reads the arguments. |
| `MapMethods` | Finds one `MapMethod` per class: first both ways of every `--pair`, then both ways of every model. |
| `MapMethod` | One `.Map()`: the full name of the class it is called on and of the class it gives. |
| `GeneratedFiles` | Writes the source of every file. |
| `GeneratedFolder` | Deletes the old files and writes the changed ones. |
| `MapGeneratorException` | Every error the tool finds itself. |

The tests are in `Tests/FantasyInformator.Tool.MapGenerator.Tests`. `GeneratedFolder` has no tests, because it uses the real disk.
