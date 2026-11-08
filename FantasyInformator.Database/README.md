# FantasyInformator.Database

The code-first database. It has its own database models and hand-written mappers to the application models. The tables match the diagram in the [Models README](../FantasyInformator.Models/README.md).

- References `FantasyInformator.Models`. Uses Entity Framework Core.
- Target: Azure SQL Database.
- The connection strings and the failed-save queue folder are in .NET user secrets. One connection string is for the user that reads and writes while the application runs (required), the other for the user that changes tables (migrations only). This one can be copied and used by migration software.

## Models and mappers

- The database models are currently a copy of every application model, with the same folders and fields.
- There is one static mapper per model, which turns a database model into an application model.
- Models link both ways: when one model holds another, the other holds it back. Mapping would go back and forth without end, so the mapper remembers what it has already mapped and reuses it.

## Tables

The settings are in the configuration classes, one per group.

The diagram is a copy of `models.drawio.svg` in the Models project.

![Models](models.drawio.svg?t=20261108003218)

- `ExternalId` is unique where a model has one, and so are the pairs that must not repeat (for example a player on a roster).
- The owner of a fantasy team and the player of a roster have no foreign key, because they can point to something not known. The columns are indexed.
- Players, NFL teams, seasons, leagues, league instances, fantasy teams and owners are never deleted. Only rostered players, league slots, slot eligibility and player positions are. Nothing depends on those, so a delete cascades to nothing.
## EF commands

Run them from the solution folder. The EF tool reads the connection string of the ef user from the user secrets.

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

## Saving

- A save writes only objects marked New, Pending or Deleted. Unchanged objects are never sent.
- Not being in a save deletes nothing. Only a Deleted object is deleted.
- The change tracker of Entity Framework is not used. The state in the application models decides.
- The global players list is saved right after it is fetched because this is informationt hat cannot be retried for a while. Saving it should not be forgotten.
- Planned: the only retry is for a failed save of data that cannot be fetched again until a timeout has passed (for example the players list). That save is written as a JSON file to the queue folder, and a retry button saves the files in the order they were written.

## Firewall

The Azure SQL server firewall must allow the PC's public IP address. Add it again when it changes.

Tests: `FantasyInformator.DatabaseTests`.
