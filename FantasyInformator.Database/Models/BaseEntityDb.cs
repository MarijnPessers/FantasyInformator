namespace FantasyInformator.Database.Models;

public abstract class BaseEntityDb
{
    public Guid Id { get; set; }

    public DateTime CreatedUtc { get; set; }

    public DateTime UpdatedUtc { get; set; }
}
