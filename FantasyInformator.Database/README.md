# FantasyInformator.Database

The code-first database. It has its own database models and hand-written mappers to the application models. The tables match the diagram in the [Models README](../FantasyInformator.Models/README.md).

- References `FantasyInformator.Models`. Uses Entity Framework Core 10 with SQL Server.
- Target: Azure SQL Database, free tier. It pauses after 15 minutes without use, and until next month when the free limit is reached, so calls need retry logic.
- Connection strings are in .NET user secrets: `Database:ConnectionStringApp` (the user that reads and writes while the application runs, required) and `Database:ConnectionStringEF` (the user that changes tables, for migrations only). `Database:FailedSaveQueueFolder` is the failed-save queue folder.
- The database is created and migrated only when the developer asks.
- The database is the source of truth. Memory changes it only when the user saves.

## Models and mappers

- `Models` is a copy of every application model, with the same folders and fields.
- `Mappers` has one static class per model with `ToModel(model)` and `ToModel(model, context)`. A null model gives null.
- A mapper maps its own fields and calls the mappers of the models it links to. `TrackedModelMapper` maps `Id`, `State` and `UpdatedUtc`.
- Models link both ways, so a `MappingContext` remembers what was already mapped.

## Tables

`FantasyInformatorDbContext` has a table per database model. The settings are in `Configurations`, one class per group.

- Text columns have a size (`ColumnSizes`). Scores are `decimal(10,4)`.
- `ExternalId` is unique where a model has one, and so are the pairs that must not repeat (for example a player on a roster).
- The owner of a fantasy team and the player of a roster have no foreign key, because they can point to something not known. The columns are indexed.
- Deleting cascades where one parent owns its children. All other links restrict the delete, because SQL Server refuses more than one cascade path.

## EF commands

Run them from the solution folder. `dotnet ef` uses `FantasyInformatorDbContextFactory`, which reads `Database:ConnectionStringEF` from the user secrets. Migrations are created and applied only when the developer asks.

One time:

```
dotnet tool install --global dotnet-ef
dotnet user-secrets set "Database:ConnectionStringEF" "<connection string of the ef user>" --project FantasyInformator.Database
```

After a change to the database models or `Configurations`:

```
dotnet ef migrations add <WhatChanged> --project FantasyInformator.Database
dotnet ef database update --project FantasyInformator.Database
```

Instead of `database update` you can use `FantasyInformator.Migrator` (see its README).

| Goal | Command |
|------|---------|
| List the migrations | `dotnet ef migrations list --project FantasyInformator.Database` |
| Undo the last migration not applied yet | `dotnet ef migrations remove --project FantasyInformator.Database` |
| Go back to an earlier migration | `dotnet ef database update <MigrationName> --project FantasyInformator.Database` |
| Make a SQL script | `dotnet ef migrations script --idempotent --project FantasyInformator.Database --output <path>` |

## Saving

- A save writes only objects marked New, Pending or Deleted. Unchanged objects are never sent.
- Not being in a save deletes nothing. Only a Deleted object is deleted, and the delete cascades.
- EF Core's change tracker is not used. The `State` in the application models decides.
- After a successful save, everything is Unchanged.
- The global players list is saved right after it is fetched.
- Planned: a failed save is written as JSON to the queue folder and retried in order.

## Firewall

Add the PC's public IP address to the Azure SQL server firewall: SQL server, Networking, Selected networks, Add your client IPv4 address. Repeat when it changes.

Tests: `FantasyInformator.DatabaseTests`.
