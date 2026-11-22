using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Leagues;
using AppLeague = FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

public static class LeagueMapper
{
    public static AppLeague.League Map(this LeagueDb league) =>
        TrackedModelMapper.MapToAppModel(league, new AppLeague.League
        {
            Name = league.Name,
            LeagueInstances = [.. league.LeagueInstances.Select(x => x.Map())],
        });

    public static LeagueDb Map(this AppLeague.League league) =>
        TrackedModelMapper.Map(league, new LeagueDb
        {
            Name = league.Name,
            LeagueInstances = [.. league.LeagueInstances.Select(x => x.Map())],
        });
}
