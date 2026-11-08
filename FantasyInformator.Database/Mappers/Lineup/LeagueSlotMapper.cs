using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Tracking;
using AppLineup = FantasyInformator.Models.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Mappers.Lineup;

public static class LeagueSlotMapper
{
    public static AppLineup.LeagueSlot? Map(this DatabaseLineup.LeagueSlot? leagueSlot) =>
        leagueSlot.Map(new MappingContext());

    public static AppLineup.LeagueSlot? Map(
        this DatabaseLineup.LeagueSlot? leagueSlot,
        MappingContext context)
    {
        if (leagueSlot is null)
        {
            return null;
        }

        if (context.TryGetMapped(leagueSlot, out AppLineup.LeagueSlot? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(leagueSlot, new AppLineup.LeagueSlot
        {
            LeagueInstanceId = leagueSlot.LeagueInstanceId,
            SlotId = leagueSlot.SlotId,
            Count = leagueSlot.Count,
        });
        context.Register(leagueSlot, mapped);

        mapped.LeagueInstance = leagueSlot.LeagueInstance.Map(context);
        mapped.Slot = leagueSlot.Slot.Map(context);

        return mapped;
    }

    public static DatabaseLineup.LeagueSlot Map(this AppLineup.LeagueSlot leagueSlot) =>
        TrackedModelMapper.FillDatabase(leagueSlot, new DatabaseLineup.LeagueSlot
        {
            LeagueInstanceId = leagueSlot.LeagueInstanceId,
            SlotId = leagueSlot.SlotId,
            Count = leagueSlot.Count,
        });
}
