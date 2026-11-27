# FantasyInformator.State

Holds the state of the application. Its handlers call Logic for the work.

## Spinners

A spinner tells the App that something is running. How the App shows it is in the App README, under "Spinners".

- `SpinnerState` holds the list of active spinners. Each has its own `SpinnerId` and an optional message.
- `IsSpinnerShown` is true while at least one spinner is active.
- `ShownSpinner` is the oldest active spinner, so its message is the one shown.
- A spinner without a message gets the App's default text, "Loading".

### A spinner for every call of an action

Let the action implement `ISpinnerAction` instead of `IAction`. `SpinnerBehavior` then starts a spinner before the handler runs and ends it when the handler is done, also when the handler throws.

Without a text, the spinner shows "Loading":

```csharp
public record LoadAllOwnersAction : ISpinnerAction;
```

With a text, give the action a `SpinnerMessage` property. The App passes the text, because the texts live in the App's `Texts.resx`:

```csharp
public record DatabaseWakeUpAction(string? SpinnerMessage = null) : ISpinnerAction;

await sender.Send(new DatabaseState.DatabaseWakeUpAction(Texts.ConnectingToDatabase), CancellationToken.None);
```

### One spinner around several calls

`sender.StartSpinnerAsync(message, cancellationToken)` starts a spinner and returns a `SpinnerScope`. With `await using`, the spinner ends when the scope ends, also when a call throws. The scope's spinner is the oldest, so its text stays on screen while the actions inside it start and end their own spinners.

Several calls under one text:

```csharp
await using var spinner = await sender.StartSpinnerAsync(Texts.LoadingLeague, CancellationToken.None);
await sender.Send(new OwnerState.LoadAllOwnersAction(), CancellationToken.None);
await sender.Send(new PlayerState.LoadAllPlayersAction(), CancellationToken.None);
```

Here "Loading league" shows the whole time, even though both actions start a "Loading" spinner of their own.
