
using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Lineups;

/// <summary>How many slots of one kind a league instance has.</summary>
public class LeagueSlotDb : TrackedModelDb
{
    public Guid LeagueInstanceId { get; set; }

    public LeagueInstanceDb? LeagueInstance { get; set; }

    public Guid SlotId { get; set; }

    public SlotDb? Slot { get; set; }

    public int Count { get; set; }
}
