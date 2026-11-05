using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class PlayerPositionMapper
{
    public static AppModels.PlayerPosition? ToModel(DatabaseModels.PlayerPosition? playerPosition) =>
        ToModel(playerPosition, new MappingContext());

    public static AppModels.PlayerPosition? ToModel(DatabaseModels.PlayerPosition? playerPosition, MappingContext context)
    {
        if (playerPosition is null)
        {
            return null;
        }

        if (context.TryGetMapped(playerPosition, out AppModels.PlayerPosition? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(playerPosition, new AppModels.PlayerPosition
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
