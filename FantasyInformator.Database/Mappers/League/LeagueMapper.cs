using FantasyInformator.Database.Mappers.Tracking;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Mappers.League;

public static class LeagueMapper
{
    public static AppLeague.League Map(this DatabaseLeague.League league) =>
        TrackedModelMapper.MapToAppModel(league, new AppLeague.League
        {
            Name = league.Name,
            LeagueInstances = [.. league.LeagueInstances.Select(x => x.Map())],
        });

    public static DatabaseLeague.League Map(this AppLeague.League league) =>
        TrackedModelMapper.MapToDbModel(league, new DatabaseLeague.League
        {
            Name = league.Name,
            LeagueInstances = [.. league.LeagueInstances.Select(x => x.Map())],
        });
}
