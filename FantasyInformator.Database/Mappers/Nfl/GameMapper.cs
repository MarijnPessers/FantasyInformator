using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class GameMapper
{
    public static AppModels.Game? ToModel(DatabaseModels.Game? game) => ToModel(game, new MappingContext());

    public static AppModels.Game? ToModel(DatabaseModels.Game? game, MappingContext context)
    {
        if (game is null)
        {
            return null;
        }

        if (context.TryGetMapped(game, out AppModels.Game? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(game, new AppModels.Game
        {
            ExternalId = game.ExternalId,
            SeasonId = game.SeasonId,
            Week = game.Week,
            HomeNflTeamId = game.HomeNflTeamId,
            AwayNflTeamId = game.AwayNflTeamId,
            KickoffUtc = game.KickoffUtc,
        });
        context.Register(game, mapped);

        mapped.Season = SeasonMapper.ToModel(game.Season, context);
        mapped.HomeNflTeam = NflTeamMapper.ToModel(game.HomeNflTeam, context);
        mapped.AwayNflTeam = NflTeamMapper.ToModel(game.AwayNflTeam, context);
        mapped.GameAppearances = context.MapList(game.GameAppearances, GameAppearanceMapper.ToModel);

        return mapped;
    }
}
