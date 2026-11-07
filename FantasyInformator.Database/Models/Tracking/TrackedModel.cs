namespace FantasyInformator.Database.Models.Tracking;

public abstract class TrackedModel
{
    public Guid Id { get; set; }

    public TrackingState State { get; set; } = TrackingState.New;

    public DateTime UpdatedUtc { get; set; }
}
