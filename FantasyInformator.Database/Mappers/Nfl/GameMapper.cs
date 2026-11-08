using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppNfl = FantasyInformator.Models.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class GameMapper
{
    public static AppNfl.Game? Map(this DatabaseNfl.Game? game) =>
        game.Map(new MappingContext());

    public static AppNfl.Game? Map(this DatabaseNfl.Game? game, MappingContext context)
    {
        if (game is null)
        {
            return null;
        }

        if (context.TryGetMapped(game, out AppNfl.Game? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(game, new AppNfl.Game
        {
            ExternalId = game.ExternalId,
            SeasonId = game.SeasonId,
            Week = game.Week,
            HomeNflTeamId = game.HomeNflTeamId,
            AwayNflTeamId = game.AwayNflTeamId,
            KickoffUtc = game.KickoffUtc,
        });
        context.Register(game, mapped);

        mapped.Season = game.Season.Map(context);
        mapped.HomeNflTeam = game.HomeNflTeam.Map(context);
        mapped.AwayNflTeam = game.AwayNflTeam.Map(context);
        mapped.GameAppearances.AddRange(
            context.MapList(game.GameAppearances, GameAppearanceMapper.Map));

        return mapped;
    }
}
