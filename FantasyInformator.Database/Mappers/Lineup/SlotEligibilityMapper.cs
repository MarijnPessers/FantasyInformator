using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppLineup = FantasyInformator.Models.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Mappers.Lineup;

public static class SlotEligibilityMapper
{
    public static AppLineup.SlotEligibility? Map(
        this DatabaseLineup.SlotEligibility? slotEligibility) =>
        slotEligibility.Map(new MappingContext());

    public static AppLineup.SlotEligibility? Map(
        this DatabaseLineup.SlotEligibility? slotEligibility,
        MappingContext context)
    {
        if (slotEligibility is null)
        {
            return null;
        }

        if (context.TryGetMapped(slotEligibility, out AppLineup.SlotEligibility? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(slotEligibility, new AppLineup.SlotEligibility
        {
            PositionId = slotEligibility.PositionId,
            SlotId = slotEligibility.SlotId,
            IsPreferred = slotEligibility.IsPreferred,
        });
        context.Register(slotEligibility, mapped);

        mapped.Position = slotEligibility.Position.Map(context);
        mapped.Slot = slotEligibility.Slot.Map(context);

        return mapped;
    }

    public static DatabaseLineup.SlotEligibility Map(this AppLineup.SlotEligibility slotEligibility) =>
        TrackedModelMapper.FillDatabase(slotEligibility, new DatabaseLineup.SlotEligibility
        {
            PositionId = slotEligibility.PositionId,
            SlotId = slotEligibility.SlotId,
            IsPreferred = slotEligibility.IsPreferred,
        });
}
