using System.Diagnostics.Metrics;
using FantasyInformator.Domain.Enums;

namespace FantasyInformator.Domain.Tracking;

public abstract class TrackedModel
{
    public Guid Id { get; set; }

    public TrackingState State { get; set; } = DefaultState.Value ?? TrackingState.New;

    /// <summary>A model read from the database matches the database, so nothing is changed yet.</summary>
    public void MarkUnchanged() => State = TrackingState.Unchanged;

    /// <summary>
    /// A new model starts with the given state until the scope ends.
    /// The earlier default is kept aside and comes back at the end.
    /// It only counts for the code that opened the scope.
    /// </summary>
    public static IDisposable SetNewDefaultState(TrackingState newState)
    {
        var oldState = DefaultState.Value;
        DefaultState.Value = newState;
        return new DefaultStateScope(oldState);
    }

    private static readonly AsyncLocal<TrackingState?> DefaultState = new();

    private sealed class DefaultStateScope(TrackingState? newState) : IDisposable
    {
        public void Dispose() => DefaultState.Value = newState;
    }
}
