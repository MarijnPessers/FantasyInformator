# FantasyInformator.Migrator

A small console program that applies the database migrations of `FantasyInformator.Database` to the database. It makes the tables, or brings them up to date. It does not create the migrations: that is `dotnet ef migrations add`, see the Database README.

## Technical notes

- References `FantasyInformator.Database` and uses its `FantasyInformatorDbContext`.
- It has its own .NET user secrets, separate from the Blazor app. The Database project has the same secrets id as the Blazor app, this program does not.
- It connects as the `ef` user (the one that may create and change tables) with `Database:ConnectionStringEF`.
- `appsettings.json` holds the default, with a `{your_password}` placeholder, and is copied next to the program. The user secrets are read after it, so they override it. `appsettings.example.json` shows every key with `<...>` placeholders and is not copied.
- It retries when the database is paused and wakes up.
- It applies only the migrations that are not applied yet, so it is safe to run again.
- It does not catch errors. If something fails, the error is shown and the program ends with a failure code.
- It is run by hand, only when the developer wants to change the database. Claude does not run it.

## Use

One time, set the connection string (use your own, never write it in a file in the solution):

```
dotnet user-secrets set "Database:ConnectionStringEF" "<connection string of the ef user>" --project FantasyInformator.Migrator
```

Then run it from the solution folder:

```
dotnet run --project FantasyInformator.Migrator
```

It prints one of three things: that there are no migrations yet (create `InitialCreate` first), that the database is up to date, or the list of migrations it applied.

No tests: the program only calls the database.
