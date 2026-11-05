using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class GameAppearanceMapper
{
    public static AppModels.GameAppearance? ToModel(DatabaseModels.GameAppearance? gameAppearance) =>
        ToModel(gameAppearance, new MappingContext());

    public static AppModels.GameAppearance? ToModel(DatabaseModels.GameAppearance? gameAppearance, MappingContext context)
    {
        if (gameAppearance is null)
        {
            return null;
        }

        if (context.TryGetMapped(gameAppearance, out AppModels.GameAppearance? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(gameAppearance, new AppModels.GameAppearance
        {
            PlayerId = gameAppearance.PlayerId,
            GameId = gameAppearance.GameId,
            IsHome = gameAppearance.IsHome,
        });
        context.Register(gameAppearance, mapped);

        mapped.Player = PlayerMapper.ToModel(gameAppearance.Player, context);
        mapped.Game = GameMapper.ToModel(gameAppearance.Game, context);
        mapped.FantasyScores = context.MapList(gameAppearance.FantasyScores, FantasyScoreMapper.ToModel);

        return mapped;
    }
}
