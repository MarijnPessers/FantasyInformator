using FantasyInformator.Database.Models.FantasyTeams;

namespace FantasyInformator.Database.Models.Lineups;

/// <summary>A place in a lineup that a player can fill, such as QB, Flex or Bench.</summary>
public class SlotDb : BaseEntityDb
{
    /// <summary>The Sleeper code of the slot, such as SUPER_FLEX.</summary>
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    public required string Abbreviation { get; set; }

    public List<LeagueSlotDb>? LeagueSlots { get; init; }

    public List<SlotEligibilityDb>? SlotEligibilities { get; init; }

    public List<RosteredPlayerDb>? RosteredPlayers { get; init; }
}
