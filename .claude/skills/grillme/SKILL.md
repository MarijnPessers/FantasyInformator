---
name: grillme
description: Grill the user on a topic, log every round in a persistent spec file named after the topic, and write the outcome there.
argument-hint: "Topic, e.g. codingpreferences"
disable-model-invocation: true
---

The text passed with `/grillme` is the topic. Without a topic, ask for one. The file is `specs/persistent/<topic>.spec.md` (create the folder when missing). When the file is new, add it to the solution file and to `specs/index.spec.md`.

If the file exists, read it first; the grilling continues from what it holds.

Call the Skill tool with "mattpocock-skills:grilling" and grill the user on the topic.

After each round, add the questions, your recommended answers and the user's answers to a "Log" section at the end of the file, so everything written can be re-read.

When the user confirms you have reached a shared understanding:
1. Wrap all content of the file that isn't in an HTML comment yet in `<!-- -->`. Never nest comments.
2. Above it, write the result: only the decisions, in plain words.
