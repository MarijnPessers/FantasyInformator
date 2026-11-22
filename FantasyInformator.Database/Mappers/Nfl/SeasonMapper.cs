using FantasyInformator.Database.Mappers.Leagues;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Nfl;
using AppNfl = FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class SeasonMapper
{
    public static AppNfl.Season Map(this SeasonDb season) =>
        TrackedModelMapper.MapToAppModel(season, new AppNfl.Season
        {
            Year = season.Year,
            LeagueInstances = [.. season.LeagueInstances.Select(x => x.Map())],
            Games = [.. season.Games.Select(x => x.Map())],
        });

    public static SeasonDb Map(this AppNfl.Season season) =>
        TrackedModelMapper.Map(season, new SeasonDb
        {
            Year = season.Year,
            LeagueInstances = [.. season.LeagueInstances.Select(x => x.Map())],
            Games = [.. season.Games.Select(x => x.Map())],
        });
}
