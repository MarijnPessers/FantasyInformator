# FantasyInformator.Shared

The application's own library of shared functionality. Each functionality that is added here gets its own explanation below, describing what it does.

[[_TOC_]]

## Functionality

- Progress reporting: how a part of the application reports errors and progress, instead of stopping with an error. The report is built. The class that receives reports, the progress state and the screen part are not.

## Explanations

### Progress reporting

- A part of the application that runs into an error deals with it itself. It doesn't pass the error up for a higher part to figure out.
- No part of the application stops with an expected error toward the part that called it, such as a website or the database not answering. An unexpected error is a bug and still stops the application, so it shows. For an expected error, the part describes what happened in a progress report and gives back a plain answer:
  - Data that was asked for: a list, which is empty when there was nothing, or no list at all (`null`) when it failed.
  - A save: `true` when it worked, `false` when it didn't.
- A progress report holds:
  - how serious it is, from Trace up to Critical
  - a header and a message
  - whether the user must see it
  - when it was made
  - the trace ID and name of the action it belongs to
  - the error itself, if there was one
- Every report goes to one place per user session: the progress state. The screen reads it and shows the reports that matter, such as warnings and errors.
- The part that gets `null` or `false` back doesn't wait for the report. It can trust that the report has arrived or will arrive, and decides by itself whether to stop or go on.
- All reports caused by one button press carry the same trace ID, so they can be shown together.
