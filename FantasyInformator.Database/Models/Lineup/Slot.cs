
using FantasyInformator.Database.Models.FantasyTeam;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Lineup;

/// <summary>A place in a lineup that a player can fill, such as QB, Flex or Bench.</summary>
public class Slot : TrackedModel
{
    /// <summary>The Sleeper code of the slot, such as SUPER_FLEX.</summary>
    public string ExternalId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Abbreviation { get; set; } = string.Empty;

    public List<LeagueSlot> LeagueSlots { get; init; } = [];

    public List<SlotEligibility> SlotEligibilities { get; init; } = [];

    public List<RosteredPlayer> RosteredPlayers { get; init; } = [];
}
