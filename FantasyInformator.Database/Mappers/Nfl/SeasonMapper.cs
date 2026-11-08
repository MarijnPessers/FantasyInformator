using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Tracking;
using AppNfl = FantasyInformator.Models.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class SeasonMapper
{
    public static AppNfl.Season? Map(this DatabaseNfl.Season? season) =>
        season.Map(new MappingContext());

    public static AppNfl.Season? Map(this DatabaseNfl.Season? season, MappingContext context)
    {
        if (season is null)
        {
            return null;
        }

        if (context.TryGetMapped(season, out AppNfl.Season? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(season, new AppNfl.Season
        {
            Year = season.Year,
        });
        context.Register(season, mapped);

        mapped.LeagueInstances.AddRange(
            context.MapList(season.LeagueInstances, LeagueInstanceMapper.Map));
        mapped.Games.AddRange(context.MapList(season.Games, GameMapper.Map));

        return mapped;
    }

    public static DatabaseNfl.Season Map(this AppNfl.Season season) =>
        TrackedModelMapper.FillDatabase(season, new DatabaseNfl.Season
        {
            Year = season.Year,
        });
}
