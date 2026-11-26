using TimeWarp.Mediator;

namespace FantasyInformator.State.Abstractions;

/// <summary>An action that shows a spinner while its handler runs.</summary>
public interface ISpinnerAction : IAction
{
    /// <summary>The text under the spinner. Null shows the default text.</summary>
    string? SpinnerMessage => null;
}
