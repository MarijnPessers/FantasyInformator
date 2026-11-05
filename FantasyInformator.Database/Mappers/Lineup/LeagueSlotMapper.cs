using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class LeagueSlotMapper
{
    public static AppModels.LeagueSlot? ToModel(DatabaseModels.LeagueSlot? leagueSlot) =>
        ToModel(leagueSlot, new MappingContext());

    public static AppModels.LeagueSlot? ToModel(DatabaseModels.LeagueSlot? leagueSlot, MappingContext context)
    {
        if (leagueSlot is null)
        {
            return null;
        }

        if (context.TryGetMapped(leagueSlot, out AppModels.LeagueSlot? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(leagueSlot, new AppModels.LeagueSlot
        {
            LeagueInstanceId = leagueSlot.LeagueInstanceId,
            SlotId = leagueSlot.SlotId,
            Count = leagueSlot.Count,
        });
        context.Register(leagueSlot, mapped);

        mapped.LeagueInstance = LeagueInstanceMapper.ToModel(leagueSlot.LeagueInstance, context);
        mapped.Slot = SlotMapper.ToModel(leagueSlot.Slot, context);

        return mapped;
    }
}
