using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppLineup = FantasyInformator.Models.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Mappers.Lineup;

public static class SlotEligibilityMapper
{
    public static AppLineup.SlotEligibility? ToModel(DatabaseLineup.SlotEligibility? slotEligibility) =>
        ToModel(slotEligibility, new MappingContext());

    public static AppLineup.SlotEligibility? ToModel(
        DatabaseLineup.SlotEligibility? slotEligibility,
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

        mapped.Position = PositionMapper.ToModel(slotEligibility.Position, context);
        mapped.Slot = SlotMapper.ToModel(slotEligibility.Slot, context);

        return mapped;
    }
}
