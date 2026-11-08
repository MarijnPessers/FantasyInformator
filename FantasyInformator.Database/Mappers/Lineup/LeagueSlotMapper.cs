using FantasyInformator.Database.Mappers.Tracking;
using AppLineup = FantasyInformator.Models.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Mappers.Lineup;

public static class LeagueSlotMapper
{
    public static AppLineup.LeagueSlot Map(this DatabaseLineup.LeagueSlot leagueSlot) =>
        TrackedModelMapper.MapToAppModel(leagueSlot, new AppLineup.LeagueSlot
        {
            LeagueInstanceId = leagueSlot.LeagueInstanceId,
            SlotId = leagueSlot.SlotId,
            Count = leagueSlot.Count,
        });

    public static DatabaseLineup.LeagueSlot Map(this AppLineup.LeagueSlot leagueSlot) =>
        TrackedModelMapper.MapToDbModel(leagueSlot, new DatabaseLineup.LeagueSlot
        {
            LeagueInstanceId = leagueSlot.LeagueInstanceId,
            SlotId = leagueSlot.SlotId,
            Count = leagueSlot.Count,
        });
}
