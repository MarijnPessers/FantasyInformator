using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class PositionMapper : EntityMapper<Position, PositionDb>
{
    protected override Position CreateCopy(PositionDb source) =>
        new()
        {
            Name = source.Name,
            Abbreviation = source.Abbreviation,
            IsDominant = source.IsDominant,
        };

    protected override PositionDb CreateCopy(Position source) =>
        new()
        {
            Name = source.Name,
            Abbreviation = source.Abbreviation,
            IsDominant = source.IsDominant,
        };
}
