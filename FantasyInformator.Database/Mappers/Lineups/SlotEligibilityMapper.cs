using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Lineups;
using AppLineup = FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

public static class SlotEligibilityMapper
{
    public static AppLineup.SlotEligibility Map(this SlotEligibilityDb slotEligibility) =>
        TrackedModelMapper.MapToAppModel(slotEligibility, new AppLineup.SlotEligibility
        {
            PositionId = slotEligibility.PositionId,
            SlotId = slotEligibility.SlotId,
            IsPreferred = slotEligibility.IsPreferred,
        });

    public static SlotEligibilityDb Map(this AppLineup.SlotEligibility slotEligibility) =>
        TrackedModelMapper.Map(slotEligibility, new SlotEligibilityDb
        {
            PositionId = slotEligibility.PositionId,
            SlotId = slotEligibility.SlotId,
            IsPreferred = slotEligibility.IsPreferred,
        });
}
