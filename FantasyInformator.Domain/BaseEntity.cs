using FantasyInformator.Domain.Enums;

namespace FantasyInformator.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public TrackingState State { get; set; } = TrackingState.New;
}
