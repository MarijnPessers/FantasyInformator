# FantasyInformator.Models

The application's own models. The database and Sleeper projects have their own models and map to these by hand.

## Technical notes

- No dependencies on other projects or packages.
- Slots and positions are tables, not enums. A position that arrives with a player and is not known is added, and the front end warns while a position has no slot link.
- IR and TAX eligibility depend on injury status and rookie years and come later.
- The slot Sleeper reports is the truth, even when it is not a preferred slot.

## Change tracking

Every object has a tracking state: New, Modified, Unchanged or Deleted.

- Each item holds a current value and, only when it differs, a pending value. The pending value is never equal to the current one.
- A New item has only a pending value. A Removed item has a pending value identical to the current one, with its state set to Removed.
- A new update overwrites the pending value. A pending value equal to the current one is dropped.
- After a save, the pending value becomes the current value.
- Every object has an updated time in UTC. When a child changes, its updated time and those of all its parents are set, bottom-up. An update without differences changes nothing.
- The global lists (players, games) always get a new updated time on an update.

A roster holds its player ids and its owner id as plain values, with no foreign key, because it may point to a player or owner that is not known.

## Models

Every model except `LastSleeperCall` inherits `TrackedModel`: a Guid `Id`, a `State` (`TrackingState`) and `UpdatedUtc`. Models that come from Sleeper have an `ExternalId`, the Sleeper id used to match Sleeper data to the own records. Every id has a navigation property, and the "one" side has a list. Sleeper data that has no field here is dropped.

The folders group the models. The namespace of every model is `FantasyInformator.Models`. The groups are in the same order in the table and in the description below the diagram: League, FantasyTeam, Player, Nfl, Lineup, Scoring, Administration.

| Folder | Model | Fields |
|--------|-------|--------|
| **League** | `League` | Name |
| | `LeagueInstance` | ExternalId, LeagueId, SeasonId. A league in one season. |
| **FantasyTeam** | `Owner` | ExternalId, Name |
| | `FantasyTeam` | ExternalId, OwnerId, LeagueInstanceId, Name |
| | `RosteredPlayer` | FantasyTeamId, PlayerId, SlotId (the slot the player is in now) |
| **Player** | `Player` | ExternalId, Name, CurrentNflTeamId (null without an NFL team) |
| | `Position` | Name, Abbreviation |
| | `PlayerPosition` | PlayerId, PositionId |
| | `GameAppearance` | PlayerId, GameId, IsHome (true = home team) |
| **Nfl** | `Season` | Year |
| | `NflTeam` | ExternalId, Name |
| | `Game` | ExternalId, SeasonId, Week, HomeNflTeamId, AwayNflTeamId, KickoffUtc |
| **Lineup** | `Slot` | ExternalId (the Sleeper code), Name, Abbreviation |
| | `LeagueSlot` | LeagueInstanceId, SlotId, Count (how many of that slot the league instance has) |
| | `SlotEligibility` | PositionId, SlotId, IsPreferred (true = preferred to start in that slot) |
| **Scoring** | `FantasyScore` | GameAppearanceId, LeagueInstanceId, Points (null = did not play, 0 = active without score) |
| | `PlayerSeasonStats` | PlayerId, LeagueInstanceId, TotalScore, GamesActive, AverageScore, AverageScoreLast3, MedianScore. Calculated by the application. |
| **Administration** | `LastSleeperCall` | CallName (the key), CalledAtUtc. No `Id`. |

The diagram is `models.drawio.svg`. Open it in draw.io to change it by hand and save it in the same file. Every model except `LastSleeperCall` also has the fields `State` and `UpdatedUtc`, which are not drawn. The table diagram is in the README of `FantasyInformator.Database`.

![Models](models.drawio.svg?t=20261105011800)

## What the diagram shows

Each coloured block is a group of models that belong together. The boxes inside it are the models, with their fields.

### League (blue)

- **League:** a fantasy league that you play in, such as "Friday Night League". Its name stays the same from year to year.
- **League instance:** that league in one season. Every year Sleeper starts the league over with a new league of its own, so each season gets a new league instance. It holds the Sleeper id of that year's league.

### FantasyTeam (green)

- **Owner:** a person in a league. An owner is not linked to a league directly. The link goes through the fantasy team they manage, and that team belongs to a league instance.
- **Fantasy team:** the team an owner has in one league instance.
- **Rostered player:** a player on that team, and the slot the player is in right now, such as QB or Bench. The same player can be in different slots in different leagues.

### Player (yellow)

- **Player:** a real football player.
- **Position:** what a player plays, such as QB or LB.
- **Player position:** links a player to the positions they play. A player has at least one.
- **Game appearance:** says that a player took part in a game, and whether they played for the home team.

### Nfl (orange)

- **Season:** one year of NFL football.
- **NFL team:** a real team, such as the Chiefs.
- **Game:** one real match between two NFL teams in a season.

### Lineup (purple)

- **Slot:** a place in a lineup, such as QB or Bench.
- **League slot:** how many of one slot a league instance has.
- **Slot eligibility:** which slots a position can start in, and which of them it prefers.

### Scoring (red)

- **Fantasy score:** the fantasy points a player earned in one game for one league instance.
- **Player season stats:** the totals and averages of those points over a season. The app works these out itself. They do not come from Sleeper.

### Administration (grey)

- **Last Sleeper call:** the last time a call to Sleeper was made, kept per call by its name. Sleeper allows the players list only once a day, so the app uses this to lock its Update button for 24 hours.

### How to read the lines

The shape at each end of a line says how many of that table are linked to one of the table at the other end.

- Two bars: exactly one. One owner, for example.
- A circle and a fork: none, one or many. An owner can have no teams, one team or many teams.
- A bar and a fork: at least one. A player always has at least one position.
- A circle and a bar: none or one. A player has no NFL team, or one.

Tests: `FantasyInformator.ModelsTests`.
