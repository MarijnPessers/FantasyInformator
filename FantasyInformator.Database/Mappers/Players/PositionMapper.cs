using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class PositionMapper : Mapper<Position, PositionDb>
{
    protected override Position CreateModel(PositionDb databaseModel) =>
        new()
        {
            Name = databaseModel.Name,
            Abbreviation = databaseModel.Abbreviation,
            IsDominant = databaseModel.IsDominant,
        };

    protected override PositionDb CreateDatabaseModel(Position model) =>
        new()
        {
            Name = model.Name,
            Abbreviation = model.Abbreviation,
            IsDominant = model.IsDominant,
        };
}
