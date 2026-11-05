using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class SlotEligibilityMapper
{
    public static AppModels.SlotEligibility? ToModel(DatabaseModels.SlotEligibility? slotEligibility) =>
        ToModel(slotEligibility, new MappingContext());

    public static AppModels.SlotEligibility? ToModel(DatabaseModels.SlotEligibility? slotEligibility, MappingContext context)
    {
        if (slotEligibility is null)
        {
            return null;
        }

        if (context.TryGetMapped(slotEligibility, out AppModels.SlotEligibility? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(slotEligibility, new AppModels.SlotEligibility
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
