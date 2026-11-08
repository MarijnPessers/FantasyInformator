using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.Tracking;
using AppLineup = FantasyInformator.Models.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Mappers.Lineup;

public static class SlotMapper
{
    public static AppLineup.Slot Map(this DatabaseLineup.Slot slot) =>
        TrackedModelMapper.MapToAppModel(slot, new AppLineup.Slot
        {
            ExternalId = slot.ExternalId,
            Name = slot.Name,
            Abbreviation = slot.Abbreviation,
            LeagueSlots = [.. slot.LeagueSlots.Select(x => x.Map())],
            SlotEligibilities = [.. slot.SlotEligibilities.Select(x => x.Map())],
            RosteredPlayers = [.. slot.RosteredPlayers.Select(x => x.Map())],
        });

    public static DatabaseLineup.Slot Map(this AppLineup.Slot slot) =>
        TrackedModelMapper.MapToDbModel(slot, new DatabaseLineup.Slot
        {
            ExternalId = slot.ExternalId,
            Name = slot.Name,
            Abbreviation = slot.Abbreviation,
            LeagueSlots = [.. slot.LeagueSlots.Select(x => x.Map())],
            SlotEligibilities = [.. slot.SlotEligibilities.Select(x => x.Map())],
            RosteredPlayers = [.. slot.RosteredPlayers.Select(x => x.Map())],
        });
}
