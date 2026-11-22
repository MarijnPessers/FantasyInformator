
using FantasyInformator.Domain.Leagues;
using FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Domain.Lineups;

/// <summary>How many slots of one kind a league instance has.</summary>
public class LeagueSlot : TrackedModel
{
    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

    public Guid SlotId { get; set; }

    public Slot? Slot { get; set; }

    public int Count { get; set; }
}
