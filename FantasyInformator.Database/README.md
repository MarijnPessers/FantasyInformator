# FantasyInformator.Database

The code-first database. It has its own database models and hand-written mappers to the application models. The tables match the diagram in the [Models README](../FantasyInformator.Models/README.md).

- References `FantasyInformator.Models`. Uses Entity Framework Core.
- Target: Azure SQL Database.
- The connection strings and the failed-save queue folder are in .NET user secrets. One connection string is for the user that reads and writes while the application runs (required), the other for the user that changes tables (migrations only). This one can be copied and used by migration software.

## Models and mappers

- The database models are currently a copy of every application model, with the same folders and fields. The one difference: they have no state, and every model holds two dates, `CreatedUtc` and `UpdatedUtc`. The dates are for database administration only and are not mapped to the application models.
- There is one static mapper per model. It turns a database model into an application model, and back, in the same way.
- A mapper maps the model itself and its children (its lists) by calling the mappers of the children. It never maps the link up to a parent, so mapping cannot loop.

## Tables

The settings of the tables are in the `Context` folder, next to the `FantasyInformatorDbContext`, one `<Group>Schema` class per group, together with `ColumnSizes`. They are not models, but they define how the models behave in the database.

The diagram is a copy of `models.drawio.svg` in the Models project, without the read-only values (such as `IsIrEligible`), because the database does not store them.

![Models](models.drawio.svg?t=20261108215943)

- `ExternalId` is unique where a model has one, and so are the pairs that must not repeat (for example a player on a roster).
- The owner of a fantasy team and the player of a roster have no foreign key, because they can point to something not known. The columns are indexed. The owner column is optional, because a team can have no owner.
- Players, NFL teams, seasons, leagues, league instances, fantasy teams and owners are never deleted. Only rostered players, league slots, slot eligibility and player positions are. Nothing depends on those, so a delete cascades to nothing.

## EF commands

Run them from the solution folder. The startup project `FantasyInformator.App` registers the context, so the EF tool takes its settings from there. The app user may not change tables, so `database update` gets the connection string of the ef user with `--connection`.

One time:

```
dotnet tool install --global dotnet-ef
```

After a change to the database models or their configuration classes:

```
dotnet ef migrations add <WhatChanged> --project FantasyInformator.Database --startup-project FantasyInformator.App
dotnet ef database update --project FantasyInformator.Database --startup-project FantasyInformator.App --connection "<connection string of the ef user>"
```

Instead of `database update` you can use `FantasyInformator.Migrator` (see its README).

## Saving

- A save writes only objects marked New, Pending or Deleted. Unchanged objects are never sent.
- The save takes a flat list of changed models and writes them in one transaction. If one fails, none is written, and the error goes up.
- `CreatedUtc` is set when a record is inserted, `UpdatedUtc` on insert and on every update, with one time for the whole save. An update never overwrites `CreatedUtc`.
- Not being in a save deletes nothing. Only a Deleted object is deleted.
- The change tracker of Entity Framework is not used. The state in the application models decides.
- The global players list is saved right after it is fetched because this is informationt hat cannot be retried for a while. Saving it should not be forgotten.
- Planned: the only retry is for a failed save of data that cannot be fetched again until a timeout has passed (for example the players list). That save is written as a JSON file to the queue folder, and a retry button saves the files in the order they were written.

## Firewall

The Azure SQL server firewall must allow the PC's public IP address. Add it again when it changes.

Tests: `FantasyInformator.Database.Tests`.
