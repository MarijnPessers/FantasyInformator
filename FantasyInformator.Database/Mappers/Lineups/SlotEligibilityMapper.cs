using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal sealed class SlotEligibilityMapper : Mapper<SlotEligibility, SlotEligibilityDb>
{
    protected override SlotEligibility CreateModel(SlotEligibilityDb databaseModel) =>
        new()
        {
            PositionId = databaseModel.PositionId,
            SlotId = databaseModel.SlotId,
            IsPreferred = databaseModel.IsPreferred,
        };

    protected override SlotEligibilityDb CreateDatabaseModel(SlotEligibility model) =>
        new()
        {
            PositionId = model.PositionId,
            SlotId = model.SlotId,
            IsPreferred = model.IsPreferred,
        };
}
