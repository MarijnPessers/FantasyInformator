# Comments

Everything in this file is a rule. Learned from rewriting the comments of the Database read and write services (2026-11-21). The Documentation rules in `CLAUDE.local.md` apply too.

## The goal

- A comment never needs explaining. If Marijn has to ask what it means, the comment has failed: rewrite it, do not explain it in chat.

## Order

- Start with why the code is there: the problem it solves. Then explain how it is solved, from the smallest piece to the bigger whole.
- Write it as problem, then solution: "We need …, but …, so …".
  - Good: "For Player we need the Func that reads all Players, but the field gives back a Delegate, which is not type specific. So the Delegate is cast to the Func for Player."
- Introduce a term only where the reader meets it in that flow. Do not start with a result the reader cannot picture yet.
  - Bad: "For each type, remembers the method that loads all of them." The reader has to imagine a loop before knowing what happens.
- When a name can be read in more than one way, the first line of its comment says how to read it.
  - Example, for SaveAllAsyncMethods: "A list of SaveAllAsync methods, one per model."

## Words

- Plain, concrete words about what happens. Use as few abstract words as possible.
- A comment makes sense without knowing the library it uses, such as EF. It says what happens, not how the library works.
- Use the project's own words: model, database model, table, Map method. Never "rows", "type", "kind", "items", "converter", "stored version" or "general form".
- Say what a thing really is. A MethodInfo is information (data) about a method, not a description (text).
- No figures of speech that can be misread, such as "blanks", "turned back" or "comes out". Say what the code does: "cast to the Func for TModel".
- Name the real code element the line uses. A comment that says "the cast" must be about a line that has a cast.
- In a `//` comment, say what a name is: "the field ReadAllAsyncMethods", "the method CreateReadAllAsyncMethod".
- Say "Throws when …", not "Fails when …", when the code throws.
- Every comment must be true. "C# can't run a Delegate" is false: it can, through `DynamicInvoke`.

## Examples

- Give an example after the general text, never instead of it.
- An example starts on its own line with "Example:".
- An example uses real names and values (Player, PlayerDb, "created on Monday, updated on Friday") and describes in words. No code in an example.

## Links

- In an XML doc comment, every reference to code is a `<see cref="…"/>`, so the reader can go there.
- Example objects are never links: Player, PlayerDb, PlayerMapper and calls like `ReadAll<Player>()` stay plain text.
- A `//` comment cannot hold a link, so it names the element and what it is (see Words).

## What a comment describes

- A comment describes only its own code. It never describes what another class does, such as what a mapper sets inside, or what a caller does with the result.
- Do not repeat what is described elsewhere. A comment that uses a field does not explain the field again; the reader can go to the field.
- A public method gets a short description of what it does. How it works goes inside the method. A method that implements an interface uses `<inheritdoc/>`.
- A comment inside a method explains the line it sits on, or the steps of that method.
- A parameter whose meaning is not obvious gets a `<param>` that says why. Example: `parameterTypes` is plural because a Map method can take the MapContext as a second input.

## Working with Marijn

- When Marijn gives wording, use it as he wrote it. Add only what he asks for, such as an example.
- Before changing a comment, read the file again; Marijn may have edited it.
- Check every new comment against this file while writing it, not only when reviewing.
- After writing a comment, review it against every rule in this file, and also on: is it clear, short enough, and not confusing?
