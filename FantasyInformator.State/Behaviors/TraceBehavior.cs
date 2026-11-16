using System.Diagnostics;
using TimeWarp.Mediator;

namespace FantasyInformator.State.Behaviors;

/// <summary>
/// Runs around every action. It runs the handler inside an activity named after the action, so every progress report
/// made while it runs carries the same trace ID and the action's name. An action sent by a click starts a new trace;
/// an action sent while another action runs stays in that action's trace.
/// </summary>
public sealed class TraceBehavior<TAction, TResponse> : IPipelineBehavior<TAction, TResponse>
    where TAction : IAction
{
    public async Task<TResponse> Handle(
        TAction request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // An activity that isn't from an action, such as the one of the Blazor connection, must not become the
        // parent, or every click would share its trace.
        if (Activity.Current?.GetCustomProperty(ActionMarker.Key) is null)
        {
            Activity.Current = null;
        }

        using var activity = new Activity(typeof(TAction).Name);
        activity.SetCustomProperty(ActionMarker.Key, true);
        _ = activity.Start();

        return await next(cancellationToken);
    }
}
