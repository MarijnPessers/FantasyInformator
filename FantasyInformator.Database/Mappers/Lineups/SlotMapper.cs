using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal sealed class SlotMapper : EntityMapper<Slot, SlotDb>
{
    protected override Slot CreateCopy(SlotDb source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
            Abbreviation = source.Abbreviation,
        };

    protected override SlotDb CreateCopy(Slot source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
            Abbreviation = source.Abbreviation,
        };
}
