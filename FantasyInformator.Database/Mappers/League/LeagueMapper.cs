using FantasyInformator.Database.Mappers.Tracking;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Mappers.League;

public static class LeagueMapper
{
    public static AppLeague.League? ToModel(DatabaseLeague.League? league) =>
        ToModel(league, new MappingContext());

    public static AppLeague.League? ToModel(DatabaseLeague.League? league, MappingContext context)
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

        mapped.LeagueInstances.AddRange(context.MapList(league.LeagueInstances, LeagueInstanceMapper.ToModel));

        return mapped;
    }
}
