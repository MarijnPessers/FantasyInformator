# Claude Instructions

## Context from the READMEs

When a question or request is not clear on its own, or you feel that more context about the application would help, read `README.md` in the solution folder first. Then read every README it links to. Do this before guessing or asking the user. When the request is clear, do not read them.

Example: "write this in the Sleeper service" names a project without saying what it does, so read the READMEs first.

## Images in the READMEs

Visual Studio caches images in the README preview. Every image link in a README ends with `?t=` and a UTC timestamp, for example `models.drawio.svg?t=20261105003433`. Each time you change a README that holds such a link, or the image file it points to, set the timestamp to the current UTC time in the format `yyyyMMddHHmmss`. Do not use a version number.

## Diagram

The models diagram is `FantasyInformator.Models/models.drawio.svg`, shown in the Models README.

- Every box lists `Id` first and, when the model has one, `ExternalId` second.
- Every line has only 90-degree corners. The exception is a line between two tables directly above and below each other: it is one straight line.
- The ends of a line are crow's feet (draw.io ER arrows), not arrows and not text. Two bars mean exactly one, a circle and a fork mean none or many, a bar and a fork mean at least one, a circle and a bar mean none or one.
- `League` and `Nfl` are at the top of the diagram, always.
- The groups (folders) have the same order in the table and in the description below the diagram: League, FantasyTeam, Player, Nfl, Lineup, Scoring, Administration. A new group goes at the end of both.
- The file has an editable part (the `content` attribute) and a drawn part, the picture that the README preview shows. Update both on every change. The drawn part is plain rectangles, text and lines, so it can be edited by hand. When a box grows or moves, also move the ends of the lines that attach to it. Opening the file in draw.io and saving it redraws the drawn part, which is how to check it.
- `FantasyInformator.Database/models.drawio.svg` is a copy of the models diagram, without the read-only fields (such as `IsIrEligible`), because the database does not store them. A read-only field shows `get` in the key column.
- The description below the diagram is plain English, without jargon.

## Preferences

- No Python on the PC. Use PowerShell or Bash for scratch scripts.
- Explain in plain English. Do not explain how a file format works unless asked.

## Transition between sessions

This section is the to-do list that carries work from one session to the next.

- At the start of a session, in the first reply, report the open next steps below and ask where the user wants to continue.
- When a step is done, remove it from the list. When new work comes up, add it. Keep the list short.
- The design decisions are in `specs/SPEC-1.md`. The READMEs describe the projects.

### State

The solution structure is set up, the NuGet packages are added and the solution builds. The application models (with their diagram in the Models README) and the settings classes per project are built. The Sleeper and Nflverse projects (Nflverse gives the kickoff times) have service interfaces, their own models and hand-written mappers to the application models, with tests, but no HTTP implementations yet (see their READMEs). The Database project has database models (a copy of the application models), mappers from them to the application models with tests, and the EF context with its table settings. The first migration is not created yet. The state model and the screens are not built yet. The design session (grilling) is finished.

### Next steps

1. The developer finishes the Azure SQL free-tier database and puts `Database:ConnectionStringApp`, `Database:ConnectionStringEF`, `Sleeper:Username` and `Database:FailedSaveQueueFolder` in .NET user secrets. Claude never reads them.
2. Sleeper and Nflverse projects: implement the service interfaces (HTTP clients), starting with the user-leagues call, with tests (MSTest, AutoFixture).
3. Database project: the developer creates the first migration with the EF commands in the Database README, only when asked. After that: the services that read and save, the mappers from the application models to the database models (not written yet), and the seed data (slots, positions, slot eligibility and NFL team names, which the developer gives).
4. Overview screen in the Blazor project. Start with the state model that holds the current and pending values (see "Change tracking" and "Open" in the spec). Then: season tabs, the update and save buttons, the report modal, a constant warning while a position has no slot link, using TimeWarp.State.

Open points to settle on the way:

- Whether TimeWarp.State actions and handlers can live in a project other than the Blazor app (not verified).
- Whether a State project or a slot-logic project is needed.
- How the same league is matched across seasons. The Sleeper docs list `previous_league_id` on a league without describing it. Check that it links a league to the previous season, then drop the name matching.
- Gaps in what Sleeper gives: `NflTeam.Name` (only an abbreviation, to be seeded). The kickoff time of a `Game` comes from the nflverse schedule (`FantasyInformator.Nflverse`).
- The per-player points in the matchups call (`players_points`) are not in the Sleeper docs. Check them in a live response.
- Two lines in the diagram are not connected to a table. Connect or delete them in draw.io.
- Whether the database users `ef`, `app` and `claude` exist yet (the developer creates them).
- The mappers return stubs for links to other records (only the key is filled). A later step must swap the stubs for the real records.
- `specs/SPEC-1.md` does not list the Nflverse project, the matchups call or the EF context yet.
- The new code (mappers, Nflverse project, Database models, mappers and EF context) has not been built or tested yet. Build and run the tests first.
- The spelling of the name: `FantasyInformator` is used everywhere, the developer once wrote "Fantasy Informator".

### Suggested tools

Matt Pocock's skills that fit this project. Mention them when the matching step starts.

- `grill-with-docs`: grilling that also updates a glossary and decision records.
- `domain-modeling`: record the terms (update, save, season, pending value) and the big choices (no AutoMapper, own change tracking).
- `tdd`: for the change tracking and the mappers.
- `prototype`: to try the overview screen and the state model first.
- `research`: to check TimeWarp.State across projects.
- `to-spec` and `to-tickets`: need an issue tracker, which the project does not have yet.
- `implement`, `wayfinder`, `handoff` and `retro`: for building, planning large work, passing on a session and improving the setup.
