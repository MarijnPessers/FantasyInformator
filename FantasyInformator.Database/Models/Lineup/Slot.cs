
using FantasyInformator.Database.Models.FantasyTeam;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Lineup;

/// <summary>A place in a lineup that a player can fill, such as QB, Flex or Bench.</summary>
public class Slot : TrackedModel
{
    /// <summary>The Sleeper code of the slot, such as SUPER_FLEX.</summary>
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    public required string Abbreviation { get; set; }

    public List<LeagueSlot> LeagueSlots { get; init; } = [];

    public List<SlotEligibility> SlotEligibilities { get; init; } = [];

    public List<RosteredPlayer> RosteredPlayers { get; init; } = [];
}
