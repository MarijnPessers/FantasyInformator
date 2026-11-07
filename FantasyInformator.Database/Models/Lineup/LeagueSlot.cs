
using FantasyInformator.Database.Models.League;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Lineup;

/// <summary>How many slots of one kind a league instance has.</summary>
public class LeagueSlot : TrackedModel
{
    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

    public Guid SlotId { get; set; }

    public Slot? Slot { get; set; }

    public int Count { get; set; }
}
