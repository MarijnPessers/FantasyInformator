using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppNfl = FantasyInformator.Models.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class GameMapper
{
    public static AppNfl.Game Map(this DatabaseNfl.Game game) =>
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

    public static DatabaseNfl.Game Map(this AppNfl.Game game) =>
        TrackedModelMapper.MapToDbModel(game, new DatabaseNfl.Game
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
