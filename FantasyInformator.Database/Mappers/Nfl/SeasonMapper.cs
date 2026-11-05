using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class SeasonMapper
{
    public static AppModels.Season? ToModel(DatabaseModels.Season? season) => ToModel(season, new MappingContext());

    public static AppModels.Season? ToModel(DatabaseModels.Season? season, MappingContext context)
    {
        if (season is null)
        {
            return null;
        }

        if (context.TryGetMapped(season, out AppModels.Season? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(season, new AppModels.Season
        {
            Year = season.Year,
        });
        context.Register(season, mapped);

        mapped.LeagueInstances = context.MapList(season.LeagueInstances, LeagueInstanceMapper.ToModel);
        mapped.Games = context.MapList(season.Games, GameMapper.ToModel);

        return mapped;
    }
}
