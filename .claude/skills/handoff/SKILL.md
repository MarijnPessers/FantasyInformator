---
name: handoff
description: Compact the current conversation into a handoff document for another agent to pick up.
argument-hint: "What will the next session be used for?"
disable-model-invocation: true
---

Write a handoff document summarising the current conversation so a fresh agent can continue the work. Save it in the solution's `specs/WIP` folder (create it when missing) as `WIP_<n>.spec.md`, where `<n>` is the lowest unused number, starting at 1. Do not add it to the solution file or the spec index. The first line is the text passed with `/handoff`, or else a description of at most 5 words.

Include a "suggested skills" section in the document, naming which skills the next agent should call the Skill tool for.

Do not duplicate content already captured in documentation read, created or changed in this session (specs, READMEs, plans, ADRs, issues). Reference it by path or URL instead. Do not open other documentation to check.

Redact any sensitive information, such as API keys, passwords, or personally identifiable information.

If the user passed text, it is also what the next session will focus on; tailor the document to it.
