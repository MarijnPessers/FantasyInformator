using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal sealed class SlotMapper : Mapper<Slot, SlotDb>
{
    protected override Slot CreateModel(SlotDb databaseModel) =>
        new()
        {
            ExternalId = databaseModel.ExternalId,
            Name = databaseModel.Name,
            Abbreviation = databaseModel.Abbreviation,
        };

    protected override SlotDb CreateDatabaseModel(Slot model) =>
        new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
            Abbreviation = model.Abbreviation,
        };
}
