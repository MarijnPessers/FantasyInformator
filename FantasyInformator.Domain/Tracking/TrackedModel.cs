using FantasyInformator.Domain.Enums;

namespace FantasyInformator.Domain.Tracking;

public abstract class TrackedModel
{
    public Guid Id { get; set; }

    public TrackingState State { get; set; } = TrackingState.New;
}
