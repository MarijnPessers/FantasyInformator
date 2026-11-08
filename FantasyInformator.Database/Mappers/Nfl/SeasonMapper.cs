using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Tracking;
using AppNfl = FantasyInformator.Models.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class SeasonMapper
{
    public static AppNfl.Season Map(this DatabaseNfl.Season season) =>
        TrackedModelMapper.MapToAppModel(season, new AppNfl.Season
        {
            Year = season.Year,
            LeagueInstances = [.. season.LeagueInstances.Select(x => x.Map())],
            Games = [.. season.Games.Select(x => x.Map())],
        });

    public static DatabaseNfl.Season Map(this AppNfl.Season season) =>
        TrackedModelMapper.MapToDbModel(season, new DatabaseNfl.Season
        {
            Year = season.Year,
            LeagueInstances = [.. season.LeagueInstances.Select(x => x.Map())],
            Games = [.. season.Games.Select(x => x.Map())],
        });
}
