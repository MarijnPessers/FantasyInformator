using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Lineups;
using AppLineup = FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

public static class LeagueSlotMapper
{
    public static AppLineup.LeagueSlot Map(this LeagueSlotDb leagueSlot) =>
        TrackedModelMapper.MapToAppModel(leagueSlot, new AppLineup.LeagueSlot
        {
            LeagueInstanceId = leagueSlot.LeagueInstanceId,
            SlotId = leagueSlot.SlotId,
            Count = leagueSlot.Count,
        });

    public static LeagueSlotDb Map(this AppLineup.LeagueSlot leagueSlot) =>
        TrackedModelMapper.Map(leagueSlot, new LeagueSlotDb
        {
            LeagueInstanceId = leagueSlot.LeagueInstanceId,
            SlotId = leagueSlot.SlotId,
            Count = leagueSlot.Count,
        });
}
