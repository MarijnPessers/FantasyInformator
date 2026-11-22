using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Lineups;
using AppLineup = FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

public static class SlotMapper
{
    public static AppLineup.Slot Map(this SlotDb slot) =>
        TrackedModelMapper.MapToAppModel(slot, new AppLineup.Slot
        {
            ExternalId = slot.ExternalId,
            Name = slot.Name,
            Abbreviation = slot.Abbreviation,
            LeagueSlots = [.. slot.LeagueSlots.Select(x => x.Map())],
            SlotEligibilities = [.. slot.SlotEligibilities.Select(x => x.Map())],
            RosteredPlayers = [.. slot.RosteredPlayers.Select(x => x.Map())],
        });

    public static SlotDb Map(this AppLineup.Slot slot) =>
        TrackedModelMapper.Map(slot, new SlotDb
        {
            ExternalId = slot.ExternalId,
            Name = slot.Name,
            Abbreviation = slot.Abbreviation,
            LeagueSlots = [.. slot.LeagueSlots.Select(x => x.Map())],
            SlotEligibilities = [.. slot.SlotEligibilities.Select(x => x.Map())],
            RosteredPlayers = [.. slot.RosteredPlayers.Select(x => x.Map())],
        });
}
