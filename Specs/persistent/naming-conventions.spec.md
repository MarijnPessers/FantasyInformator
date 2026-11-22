# Naming conventions

Everything in this file is a rule. Learned from the Database read and write services (2026-11-21).

## What a name says

- A name says exactly what it holds or makes. Never use vague words such as Call, Build, Row, Helper, Project, Everything or General.
  - Good: `ReadAllAsyncMethods` (holds ReadAllAsync methods), `MapMethods` (holds Map methods), `ReadAllMethodInfo` (holds a MethodInfo).
  - Bad: `ReadAllCalls`, `RowMappers`, `ReadAllMethod` (it is not a method).
- A method name is the action plus what it gives back.
  - Good: `CreateReadAllAsyncMethod`, `FindMapMethod`.
  - Bad: `BuildReadAllCall`, `FindRowMapper`.
- A name makes sense without knowing the library behind it. It says what the code does, not which library method it uses.
  - Good: `CreateInListFilter` (the filter keeps entries whose value is in a list).
  - Bad: `CreateContainsFilter` (only readable if you know the LINQ method Contains).
- The words of a name must not run together into another meaning.
  - Bad: `CreateValueInListFilter`, which reads as a filter for a "CreateValue".
  - When a name can still be read in more than one way, the first line of its comment says how to read it (see `comments.spec.md`).
- A class that holds a list is named after what is in the list.
  - Good: `DatabaseModelTypes`, `MapperTypes`.
  - Bad: `DatabaseProject`, `ProjectTypes`.
- A variable names the thing it holds: `mapper`, `list`. A list that a test fills with failures says what is missing: `modelsWithoutDatabaseModel`, not `modelsWithout`.

## Names that belong together

- Related names repeat the same words, so they visibly belong together: `ReadAll`, `ReadAllAsync`, `ReadAllAsyncMethods`, `CreateReadAllAsyncMethod`, `ReadAllMethodInfo`.
- A service that mirrors another uses the mirrored names: `SaveAll`, `SaveAllAsync`, `SaveAllAsyncMethods` next to the read names.

## Words of this project

- A model is a Domain class, such as `Player`. A database model is its Database class, such as `PlayerDb`. Never say "type", "kind", "row" or "table version" for these.
- Call a field a field, a property a property and a method a method.

## C# patterns

- A method that starts with `Try` returns a `bool` and gives its value through `out`. A method that returns `null` when it finds nothing ends in `OrNull`.
- Put a member in the class whose job it is. A list of all database models does not belong in the class that looks up Map methods.

## Marijn's names

- When Marijn renames something, keep his name and use it everywhere it applies.
