using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class GameAppearanceMapper
{
    public static AppPlayer.GameAppearance? Map(this DatabasePlayer.GameAppearance? gameAppearance) =>
        gameAppearance.Map(new MappingContext());

    public static AppPlayer.GameAppearance? Map(
        this DatabasePlayer.GameAppearance? gameAppearance,
        MappingContext context)
    {
        if (gameAppearance is null)
        {
            return null;
        }

        if (context.TryGetMapped(gameAppearance, out AppPlayer.GameAppearance? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(gameAppearance, new AppPlayer.GameAppearance
        {
            PlayerId = gameAppearance.PlayerId,
            GameId = gameAppearance.GameId,
            IsHome = gameAppearance.IsHome,
        });
        context.Register(gameAppearance, mapped);

        mapped.Player = gameAppearance.Player.Map(context);
        mapped.Game = gameAppearance.Game.Map(context);
        mapped.FantasyScores.AddRange(
            context.MapList(gameAppearance.FantasyScores, FantasyScoreMapper.Map));

        return mapped;
    }
}
