using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class PlayerPositionMapper : Mapper<PlayerPosition, PlayerPositionDb>
{
    protected override PlayerPosition CreateModel(PlayerPositionDb databaseModel) =>
        new()
        {
            PlayerId = databaseModel.PlayerId,
            PositionId = databaseModel.PositionId,
        };

    protected override PlayerPositionDb CreateDatabaseModel(PlayerPosition model) =>
        new()
        {
            PlayerId = model.PlayerId,
            PositionId = model.PositionId,
        };
}
