using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.Tracking;
using AppLineup = FantasyInformator.Models.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Mappers.Lineup;

public static class SlotMapper
{
    public static AppLineup.Slot? ToModel(DatabaseLineup.Slot? slot) =>
        ToModel(slot, new MappingContext());

    public static AppLineup.Slot? ToModel(DatabaseLineup.Slot? slot, MappingContext context)
    {
        if (slot is null)
        {
            return null;
        }

        if (context.TryGetMapped(slot, out AppLineup.Slot? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(slot, new AppLineup.Slot
        {
            ExternalId = slot.ExternalId,
            Name = slot.Name,
            Abbreviation = slot.Abbreviation,
        });
        context.Register(slot, mapped);

        mapped.LeagueSlots.AddRange(context.MapList(slot.LeagueSlots, LeagueSlotMapper.ToModel));
        mapped.SlotEligibilities.AddRange(context.MapList(slot.SlotEligibilities, SlotEligibilityMapper.ToModel));
        mapped.RosteredPlayers.AddRange(context.MapList(slot.RosteredPlayers, RosteredPlayerMapper.ToModel));

        return mapped;
    }
}
