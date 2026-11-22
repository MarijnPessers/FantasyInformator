using FantasyInformator.Database.Mappers.Players;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Nfl;
using AppNfl = FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class GameMapper
{
    public static AppNfl.Game Map(this GameDb game) =>
        TrackedModelMapper.MapToAppModel(game, new AppNfl.Game
        {
            ExternalId = game.ExternalId,
            SeasonId = game.SeasonId,
            Week = game.Week,
            HomeNflTeamId = game.HomeNflTeamId,
            AwayNflTeamId = game.AwayNflTeamId,
            KickoffUtc = game.KickoffUtc,
            GameAppearances = [.. game.GameAppearances.Select(x => x.Map())],
        });

    public static GameDb Map(this AppNfl.Game game) =>
        TrackedModelMapper.Map(game, new GameDb
        {
            ExternalId = game.ExternalId,
            SeasonId = game.SeasonId,
            Week = game.Week,
            HomeNflTeamId = game.HomeNflTeamId,
            AwayNflTeamId = game.AwayNflTeamId,
            KickoffUtc = game.KickoffUtc,
            GameAppearances = [.. game.GameAppearances.Select(x => x.Map())],
        });
}
