using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Players;
using AppPlayer = FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

public static class GameAppearanceMapper
{
    public static AppPlayer.GameAppearance Map(this GameAppearanceDb gameAppearance) =>
        TrackedModelMapper.MapToAppModel(gameAppearance, new AppPlayer.GameAppearance
        {
            PlayerId = gameAppearance.PlayerId,
            GameId = gameAppearance.GameId,
            IsHome = gameAppearance.IsHome,
            FantasyScores = [.. gameAppearance.FantasyScores.Select(x => x.Map())],
        });

    public static GameAppearanceDb Map(this AppPlayer.GameAppearance gameAppearance) =>
        TrackedModelMapper.Map(gameAppearance, new GameAppearanceDb
        {
            PlayerId = gameAppearance.PlayerId,
            GameId = gameAppearance.GameId,
            IsHome = gameAppearance.IsHome,
            FantasyScores = [.. gameAppearance.FantasyScores.Select(x => x.Map())],
        });
}
