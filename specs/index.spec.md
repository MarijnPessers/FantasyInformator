# Spec index

Everything in this file is a rule.

## Persistent specs

Read a spec file only when its "Read when" fits the current task, or when Marijn names it. When a listed file is missing, say so before going on.

| File | Content | Read when |
|------|---------|-----------|
| `persistent/conventions.spec.md` | Conventions: project structure, state, errors and web calls, classes, Blazor, texts, mapping and tests. | Writing or reviewing code. |
| `persistent/nugetcheck.spec.md` | NuGet check: the date the prerelease packages were last checked for a newer stable version. | At the start of each session. |
| `persistent/readme-images.spec.md` | Rules for README visuals: Markdown before draw.io, where image files go, the timestamp on image links, and how the models diagram (draw.io) is drawn and kept in sync. | Adding or changing a visual in a README. |
| `persistent/roadmap.spec.md` | Roadmap: the features still to come. | Planning or designing a new feature. |

## Keeping the specs up to date

- When a persistent spec file is added, suggest an entry in the table above with a description and a "Read when". When a spec file outgrows its description, suggest a new description.
- When adding or removing a spec file, except a WIP file, add it to or remove it from `FantasyInformator.slnx`.

## Spec lists

- Work through spec lists one item at a time, group by group. After each item, report what is left: "<total> left (<group>: <n>)".
- Mark a finished spec item with ✅; never delete it. When every item in a spec is done, offer README additions, then delete the spec.

## WIP files

- When asked about WIP files, or to load one without a number, list each file as `<filename>: <first line>`, unnumbered. On a number, read `specs/WIP/WIP_<number>.spec.md`, then show only the request for "implement" to delete it. Never renumber WIP files.

## NuGet check

- If `persistent/nugetcheck.spec.md` is missing, or its last-check date is 14 or more days ago, look up every prerelease NuGet package (a version with a suffix like `-beta`, `-preview` or `-rc`). Suggest an update for each one that has a newer stable version. Then write today's date in the file; that needs no "implement".
