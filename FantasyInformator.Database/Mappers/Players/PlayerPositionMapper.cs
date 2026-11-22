using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Players;
using AppPlayer = FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

public static class PlayerPositionMapper
{
    public static AppPlayer.PlayerPosition Map(this PlayerPositionDb playerPosition) =>
        TrackedModelMapper.MapToAppModel(playerPosition, new AppPlayer.PlayerPosition
        {
            PlayerId = playerPosition.PlayerId,
            PositionId = playerPosition.PositionId,
        });

    public static PlayerPositionDb Map(this AppPlayer.PlayerPosition playerPosition) =>
        TrackedModelMapper.Map(playerPosition, new PlayerPositionDb
        {
            PlayerId = playerPosition.PlayerId,
            PositionId = playerPosition.PositionId,
        });
}
