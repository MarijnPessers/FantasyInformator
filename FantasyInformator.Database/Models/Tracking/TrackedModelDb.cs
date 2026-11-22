namespace FantasyInformator.Database.Models.Tracking;

public abstract class TrackedModelDb
{
    public Guid Id { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime UpdatedUtc { get; set; }
}
