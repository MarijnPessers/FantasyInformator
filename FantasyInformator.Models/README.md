# FantasyInformator.Models

The application's own models.

## Change tracking

Every model except `LastSleeperCall` has three base fields:

- `Id`: a Guid.
- `State`: `New`, `Pending`, `Unchanged` or `Deleted`.
- `UpdatedUtc`: when the model last changed.

## Models

Some models also have an `ExternalId`, a separate value used only for matching records.

The diagram is `models.drawio.svg`. The base fields are not drawn.

Each box has three columns: the field name, its type (with the length for text and the digits for decimals), and its key. `PK` is the primary key, `FK` a link to another table and `UK` a value that must be unique.

![Models](models.drawio.svg?t=20261107230338)

## What the diagram shows

Each coloured block is a group of models that belong together.

### League (blue)

- **League:** a fantasy league you play in. Its name stays the same from year to year.
- **League instance:** that league in one season.

### FantasyTeam (green)

- **Owner:** a person in a league.
- **Fantasy team:** the team an owner has in one league instance. The owner is a plain id, because it may point to an owner that is not known.
- **Rostered player:** a player on that team, and the slot the player is in right now. The player is a plain id, because it may point to a player that is not known.

### Player (yellow)

- **Player:** a real football player. Without an NFL team, the current team is empty.
- **Position:** what a player plays, such as QB or LB.
- **Player position:** links a player to the positions they play. A player has at least one.
- **Game appearance:** a player took part in a game, for the home or the away team.

### Nfl (orange)

- **Season:** one year of NFL football.
- **NFL team:** a real team, such as the Chiefs.
- **Game:** one real match between two NFL teams in a season.

### Lineup (purple)

- **Slot:** a place in a lineup, such as QB or Bench.
- **League slot:** how many of one slot a league instance has.
- **Slot eligibility:** which slots a position can start in, and which it prefers.

### Scoring (red)

- **Fantasy score:** the points a player earned in one game for one league instance. Empty means the player did not play, 0 means active without a score.
- **Player season stats:** totals and averages of those points over a season. The app works these out itself.

### Administration (grey)

- **Last Sleeper call:** the last time a call was made, kept per call name, which is its key. It has no `Id`.
