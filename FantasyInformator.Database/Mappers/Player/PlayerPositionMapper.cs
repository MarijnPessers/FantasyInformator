using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PlayerPositionMapper
{
    public static AppPlayer.PlayerPosition? Map(this DatabasePlayer.PlayerPosition? playerPosition) =>
        playerPosition.Map(new MappingContext());

    public static AppPlayer.PlayerPosition? Map(
        this DatabasePlayer.PlayerPosition? playerPosition,
        MappingContext context)
    {
        if (playerPosition is null)
        {
            return null;
        }

        if (context.TryGetMapped(playerPosition, out AppPlayer.PlayerPosition? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(playerPosition, new AppPlayer.PlayerPosition
        {
            PlayerId = playerPosition.PlayerId,
            PositionId = playerPosition.PositionId,
        });
        context.Register(playerPosition, mapped);

        mapped.Player = playerPosition.Player.Map(context);
        mapped.Position = playerPosition.Position.Map(context);

        return mapped;
    }

    public static DatabasePlayer.PlayerPosition Map(this AppPlayer.PlayerPosition playerPosition) =>
        TrackedModelMapper.FillDatabase(playerPosition, new DatabasePlayer.PlayerPosition
        {
            PlayerId = playerPosition.PlayerId,
            PositionId = playerPosition.PositionId,
        });
}
