using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Domain.Lineups;

/// <summary>A place in a lineup that a player can fill, such as QB, Flex or Bench.</summary>
public class Slot : BaseEntity
{
    /// <summary>The Sleeper code of the slot, such as SUPER_FLEX.</summary>
    public required string ExternalId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Abbreviation { get; set; } = string.Empty;

    public List<LeagueSlot>? LeagueSlots { get; init; }

    public List<SlotEligibility>? SlotEligibilities { get; init; }

    public List<RosteredPlayer>? RosteredPlayers { get; init; }
}
