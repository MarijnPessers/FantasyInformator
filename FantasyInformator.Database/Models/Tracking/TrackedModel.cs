namespace FantasyInformator.Database.Models.Tracking;

public abstract class TrackedModel
{
    public Guid Id { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime UpdatedUtc { get; set; }
}
