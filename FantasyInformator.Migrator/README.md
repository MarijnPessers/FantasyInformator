# FantasyInformator.Migrator

A console program with a menu to manage the database migrations of `FantasyInformator.Database`.

## What it does

- **Create a migration:** asks for a name and creates it. If the model has no changes, nothing is created.
- **Apply all migrations:** applies every migration that is not applied yet.
- **Apply or roll back to a migration:** pick a migration from a list (9 at a time) to go to it.
- **Exit.**

If something fails, the error is shown and the menu appears again.

## What it needs

- The `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`.
- The connection string of the `ef` user (the one that may create and change tables), in `Database:ConnectionStringEF`. `appsettings.json` has it with a `{your_password}` placeholder. Do not put the real password there, because the file is in git. Use user secrets (`secrets.json`), which override `appsettings.json`:

  ```
  dotnet user-secrets set "Database:ConnectionStringEF" "<connection string of the ef user>" --project FantasyInformator.Migrator
  ```

- The setting `Migrator:DatabaseProjectPath` in `appsettings.json`, the folder of the Database project. The default works. `appsettings.example.json` shows every key.
- An IP address that the firewall of the Azure SQL server allows.

## Run

```
dotnet run --project FantasyInformator.Migrator
```