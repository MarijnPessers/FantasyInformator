using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal sealed class SlotEligibilityMapper : EntityMapper<SlotEligibility, SlotEligibilityDb>
{
    protected override SlotEligibility CreateCopy(SlotEligibilityDb source) =>
        new()
        {
            PositionId = source.PositionId,
            SlotId = source.SlotId,
            IsPreferred = source.IsPreferred,
        };

    protected override SlotEligibilityDb CreateCopy(SlotEligibility source) =>
        new()
        {
            PositionId = source.PositionId,
            SlotId = source.SlotId,
            IsPreferred = source.IsPreferred,
        };
}
