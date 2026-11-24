# FantasyInformator.Tests

Tests that run a chosen flow through State and Logic, to check that those projects work together. Not every flow gets one.

[[_TOC_]]

## Flows

- Saving the player data. Sleeper allows the players call only once every 24 hours, so fetched players must never get lost, not even when the database fails.

## How a flow test runs

- State and Logic run their real code, set up the same way the app sets them up.
- Sleeper and the database are stand-ins. A test decides what they answer, such as a list of players or a failed save.
