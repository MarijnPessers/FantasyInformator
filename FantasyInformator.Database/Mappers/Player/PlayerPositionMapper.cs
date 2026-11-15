using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Domain.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PlayerPositionMapper
{
    public static AppPlayer.PlayerPosition Map(this DatabasePlayer.PlayerPosition playerPosition) =>
        TrackedModelMapper.MapToAppModel(playerPosition, new AppPlayer.PlayerPosition
        {
            PlayerId = playerPosition.PlayerId,
            PositionId = playerPosition.PositionId,
        });

    public static DatabasePlayer.PlayerPosition Map(this AppPlayer.PlayerPosition playerPosition) =>
        TrackedModelMapper.MapToDbModel(playerPosition, new DatabasePlayer.PlayerPosition
        {
            PlayerId = playerPosition.PlayerId,
            PositionId = playerPosition.PositionId,
        });
}
