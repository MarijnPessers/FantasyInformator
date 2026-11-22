# Spec index

Everything in this file is a rule.

## Persistent specs

Read a spec file only when its "Read when" fits the current task, or when Marijn names it. When a listed file is missing, say so before going on.

| File | Content | Read when |
|------|---------|-----------|
| `persistent/conventions.spec.md` | Conventions: project structure, state, errors and web calls, classes, Blazor, texts, mapping and tests. | Writing or reviewing code. |
| `persistent/naming-conventions.spec.md` | Naming: how classes, members and variables are named so the name says what it holds or does, and which names belong together. | Writing or reviewing code. |
| `persistent/comments.spec.md` | Comments: their order (problem first), wording, examples, links and what a comment may describe. | Writing or reviewing code, including comments. Read it again right before writing or reviewing the comments of a whole file. |
| `persistent/nugetcheck.spec.md` | NuGet check: the date the prerelease packages were last checked for a newer stable version. | At the start of each session. |
| `persistent/readme-images.spec.md` | Rules for README visuals: Markdown before draw.io, where image files go, the timestamp on image links, and how the models diagram (draw.io) is drawn and kept in sync. | Adding or changing a visual in a README. |
| `persistent/roadmap.spec.md` | Roadmap: the features still to come. | Planning or designing a new feature. |
| `persistent/timewarp-state.spec.md` | TimeWarp.State and TimeWarp.Mediator: version in use, where to look, registering, pipelines, states, actions and handlers, behaviors, tracing, Blazor, other packages. | Needing to know anything about TimeWarp.State. |

## Keeping the specs up to date

- When a persistent spec file is added, suggest an entry in the table above with a description and a "Read when". When a spec file outgrows its description, suggest a new description.
- When adding or removing a spec file, except a WIP file, add it to or remove it from `FantasyInformator.slnx`.

## Spec lists

- Work through spec lists one item at a time, group by group. After each item, report what is left: "<total> left (<group>: <n>)".
- Mark a finished spec item with ✅; never delete it. When every item in a spec is done, offer README additions, then delete the spec.

## WIP files

- Never read a WIP file, except `WIP_<number>.spec.md` after Marijn gives its number.
- When asked about WIP files, or to load one without a number, list each file as `<filename>: <first line>`, unnumbered. On a number, read `specs/WIP/WIP_<number>.spec.md` and work through its items. Mark each item ✅ in the file as soon as it's done; that needs no "implement". When every item is done, offer to delete the file. Never renumber WIP files.
- When `/handoff` runs while a WIP file exists, ask whether to write a new WIP file or update the existing one.

## NuGet check

- If `persistent/nugetcheck.spec.md` is missing, or its last-check date is 14 or more days ago, look up every prerelease NuGet package (a version with a suffix like `-beta`, `-preview` or `-rc`). Suggest an update for each one that has a newer stable version. Then write today's date in the file; that needs no "implement".

## TimeWarp.State

- When you need to know something about TimeWarp.State, read `persistent/timewarp-state.spec.md` first. Look on the web only when it doesn't hold the answer.
- Add every new fact to that spec, with its source. Writing to it needs no "implement".
