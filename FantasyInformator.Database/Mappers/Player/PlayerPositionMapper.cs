using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PlayerPositionMapper
{
    public static AppPlayer.PlayerPosition? ToModel(DatabasePlayer.PlayerPosition? playerPosition) =>
        ToModel(playerPosition, new MappingContext());

    public static AppPlayer.PlayerPosition? ToModel(
        DatabasePlayer.PlayerPosition? playerPosition,
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

        mapped.Player = PlayerMapper.ToModel(playerPosition.Player, context);
        mapped.Position = PositionMapper.ToModel(playerPosition.Position, context);

        return mapped;
    }
}
