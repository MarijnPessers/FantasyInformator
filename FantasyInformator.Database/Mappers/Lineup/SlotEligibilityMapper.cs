using FantasyInformator.Database.Mappers.Tracking;
using AppLineup = FantasyInformator.Models.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Mappers.Lineup;

public static class SlotEligibilityMapper
{
    public static AppLineup.SlotEligibility Map(this DatabaseLineup.SlotEligibility slotEligibility) =>
        TrackedModelMapper.MapToAppModel(slotEligibility, new AppLineup.SlotEligibility
        {
            PositionId = slotEligibility.PositionId,
            SlotId = slotEligibility.SlotId,
            IsPreferred = slotEligibility.IsPreferred,
        });

    public static DatabaseLineup.SlotEligibility Map(this AppLineup.SlotEligibility slotEligibility) =>
        TrackedModelMapper.MapToDbModel(slotEligibility, new DatabaseLineup.SlotEligibility
        {
            PositionId = slotEligibility.PositionId,
            SlotId = slotEligibility.SlotId,
            IsPreferred = slotEligibility.IsPreferred,
        });
}
