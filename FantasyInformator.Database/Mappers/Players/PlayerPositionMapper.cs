using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class PlayerPositionMapper : EntityMapper<PlayerPosition, PlayerPositionDb>
{
    protected override PlayerPosition CreateCopy(PlayerPositionDb source) =>
        new()
        {
            PlayerId = source.PlayerId,
            PositionId = source.PositionId,
        };

    protected override PlayerPositionDb CreateCopy(PlayerPosition source) =>
        new()
        {
            PlayerId = source.PlayerId,
            PositionId = source.PositionId,
        };
}
