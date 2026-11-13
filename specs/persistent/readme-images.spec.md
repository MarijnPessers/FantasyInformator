# READMEs

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
