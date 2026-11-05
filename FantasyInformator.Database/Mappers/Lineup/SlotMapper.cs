using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class SlotMapper
{
    public static AppModels.Slot? ToModel(DatabaseModels.Slot? slot) => ToModel(slot, new MappingContext());

    public static AppModels.Slot? ToModel(DatabaseModels.Slot? slot, MappingContext context)
    {
        if (slot is null)
        {
            return null;
        }

        if (context.TryGetMapped(slot, out AppModels.Slot? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(slot, new AppModels.Slot
        {
            ExternalId = slot.ExternalId,
            Name = slot.Name,
            Abbreviation = slot.Abbreviation,
        });
        context.Register(slot, mapped);

        mapped.LeagueSlots = context.MapList(slot.LeagueSlots, LeagueSlotMapper.ToModel);
        mapped.SlotEligibilities = context.MapList(slot.SlotEligibilities, SlotEligibilityMapper.ToModel);
        mapped.RosteredPlayers = context.MapList(slot.RosteredPlayers, RosteredPlayerMapper.ToModel);

        return mapped;
    }
}
