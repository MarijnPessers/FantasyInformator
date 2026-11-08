using FantasyInformator.Database.Mappers.Tracking;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Mappers.League;

public static class LeagueMapper
{
    public static AppLeague.League? Map(this DatabaseLeague.League? league) =>
        league.Map(new MappingContext());

    public static AppLeague.League? Map(this DatabaseLeague.League? league, MappingContext context)
    {
        if (league is null)
        {
            return null;
        }

        if (context.TryGetMapped(league, out AppLeague.League? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(league, new AppLeague.League
        {
            Name = league.Name,
        });
        context.Register(league, mapped);

        mapped.LeagueInstances.AddRange(
            context.MapList(league.LeagueInstances, LeagueInstanceMapper.Map));

        return mapped;
    }

    public static DatabaseLeague.League Map(this AppLeague.League league) =>
        TrackedModelMapper.FillDatabase(league, new DatabaseLeague.League
        {
            Name = league.Name,
        });
}
