using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class GameAppearanceMapper
{
    public static AppPlayer.GameAppearance Map(this DatabasePlayer.GameAppearance gameAppearance) =>
        TrackedModelMapper.MapToAppModel(gameAppearance, new AppPlayer.GameAppearance
        {
            PlayerId = gameAppearance.PlayerId,
            GameId = gameAppearance.GameId,
            IsHome = gameAppearance.IsHome,
            FantasyScores = [.. gameAppearance.FantasyScores.Select(x => x.Map())],
        });

    public static DatabasePlayer.GameAppearance Map(this AppPlayer.GameAppearance gameAppearance) =>
        TrackedModelMapper.MapToDbModel(gameAppearance, new DatabasePlayer.GameAppearance
        {
            PlayerId = gameAppearance.PlayerId,
            GameId = gameAppearance.GameId,
            IsHome = gameAppearance.IsHome,
            FantasyScores = [.. gameAppearance.FantasyScores.Select(x => x.Map())],
        });
}
