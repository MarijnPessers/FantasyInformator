# FantasyInformator.Database

The code-first database and the services that read and save. It has its own database models and mappers (written by hand, no AutoMapper) to the application models.

## Technical notes

- References `FantasyInformator.Models`. Uses Entity Framework Core 10 with the SQL Server provider (`Microsoft.EntityFrameworkCore.SqlServer`), and the design package for migrations.
- Target database: Azure SQL Database, free tier (General Purpose, serverless). When the free limit is reached it pauses until next month, and it also pauses itself after 15 minutes without use. Calls therefore need retry logic.
- The connection string is kept in .NET user secrets, outside the solution folder. There are two. `Database:ConnectionStringApp` is the user that reads and writes while the application runs (required, checked at start by `AddDatabaseOptions`). `Database:ConnectionStringEF` is the user that creates and changes tables, used for migrations only. `Database:FailedSaveQueueFolder` is the folder of the failed-save queue.
- The database is created and migrated only when the developer asks for it.
- The database is the source of truth. Data in memory only changes the database when the user saves.

## Model overview

Every table also has a `State` and an `UpdatedUtc` (not drawn). `ExternalId` is the Sleeper id, used to match Sleeper data to the own records.

```mermaid
erDiagram
    League ||--o{ LeagueInstance : has
    Season ||--o{ LeagueInstance : has
    Season ||--o{ Game : has
    LeagueInstance ||--o{ FantasyTeam : has
    Owner ||--o{ FantasyTeam : owns
    NflTeam ||--o{ Game : "plays home"
    NflTeam ||--o{ Game : "plays away"
    NflTeam |o--o{ Player : "current team of"
    FantasyTeam ||--o{ RosteredPlayer : has
    Player ||--o{ RosteredPlayer : "is on"
    Player ||--o{ GameAppearance : plays
    Game ||--o{ GameAppearance : has
    GameAppearance ||--o{ FantasyScore : scores
    LeagueInstance ||--o{ FantasyScore : "scored for"
    LeagueInstance ||--o{ LeagueSlot : has
    Slot ||--o{ LeagueSlot : "used in"
    Slot ||--o{ RosteredPlayer : "filled by"
    Player ||--o{ PlayerPosition : has
    Position ||--o{ PlayerPosition : "played by"
    Position ||--o{ SlotEligibility : "can start in"
    Slot ||--o{ SlotEligibility : "filled by"
    Player ||--o{ PlayerSeasonStats : has
    LeagueInstance ||--o{ PlayerSeasonStats : "calculated for"

    League {
        Guid Id PK
        string Name
    }
    LeagueInstance {
        Guid Id PK
        string ExternalId
        Guid LeagueId FK
        Guid SeasonId FK
    }
    Season {
        Guid Id PK
        int Year
    }
    Owner {
        Guid Id PK
        string ExternalId
        string Name
    }
    FantasyTeam {
        Guid Id PK
        string ExternalId
        Guid OwnerId FK
        Guid LeagueInstanceId FK
        string Name
    }
    NflTeam {
        Guid Id PK
        string ExternalId
        string Name
    }
    Game {
        Guid Id PK
        string ExternalId
        Guid SeasonId FK
        int Week
        Guid HomeNflTeamId FK
        Guid AwayNflTeamId FK
        datetime KickoffUtc
    }
    Player {
        Guid Id PK
        string ExternalId
        string Name
        Guid CurrentNflTeamId FK "null without an NFL team"
    }
    Slot {
        Guid Id PK
        string ExternalId "the Sleeper code"
        string Name
        string Abbreviation
    }
    LeagueSlot {
        Guid Id PK
        Guid LeagueInstanceId FK
        Guid SlotId FK
        int Count "how many of that slot"
    }
    Position {
        Guid Id PK
        string Name
        string Abbreviation
    }
    PlayerPosition {
        Guid Id PK
        Guid PlayerId FK
        Guid PositionId FK
    }
    SlotEligibility {
        Guid Id PK
        Guid PositionId FK
        Guid SlotId FK
        bool IsPreferred "preferred to start in this slot"
    }
    LastSleeperCall {
        string CallName PK
        datetime CalledAtUtc
    }
    GameAppearance {
        Guid Id PK
        Guid PlayerId FK
        Guid GameId FK
        bool IsHome "true = home team"
    }
    FantasyScore {
        Guid Id PK
        Guid GameAppearanceId FK
        Guid LeagueInstanceId FK
        decimal Points "null = did not play, 0 = active without score"
    }
    RosteredPlayer {
        Guid Id PK
        Guid FantasyTeamId FK
        Guid PlayerId FK
        Guid SlotId FK "the slot the player is in now"
    }
    PlayerSeasonStats {
        Guid Id PK
        Guid PlayerId FK
        Guid LeagueInstanceId FK
        decimal TotalScore "null = no game played yet"
        int GamesActive
        decimal AverageScore
        decimal AverageScoreLast3
        decimal MedianScore
    }
```

`PlayerSeasonStats` is calculated by the application from `FantasyScore` and is never retrieved from Sleeper. Its season comes through the league instance.

## Saving

- The save writes only objects marked New, Modified or Deleted, one statement per changed object.
- A changed parent does not update its unchanged children. Unchanged objects are never sent to the database.
- Being absent from a save never deletes anything. Only an object marked Deleted is deleted, and a delete cascades.
- A New complex object inserts records in all the tables it spans.
- EF Core's own change tracker is not used to decide what changed. The tracking state in the application models decides.
- After a successful save, everything is reset to Unchanged.
- The global players list is the exception: it is saved immediately after it is fetched.
- Player ids and owner ids on a roster have no foreign key.
- Planned: when a save fails, the data is written as a JSON file to a queue folder (named by UTC timestamp with microseconds) and processed in order on retry.

## Firewall

Add the PC's public IP address to the Azure SQL server firewall: SQL server, Networking, Selected networks, Add your client IPv4 address. Repeat when the IP address changes.

Tests: `FantasyInformator.DatabaseTests`.
