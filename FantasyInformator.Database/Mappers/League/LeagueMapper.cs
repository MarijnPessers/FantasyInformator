using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class LeagueMapper
{
    public static AppModels.League? ToModel(DatabaseModels.League? league) => ToModel(league, new MappingContext());

    public static AppModels.League? ToModel(DatabaseModels.League? league, MappingContext context)
    {
        if (league is null)
        {
            return null;
        }

        if (context.TryGetMapped(league, out AppModels.League? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(league, new AppModels.League
        {
            Name = league.Name,
        });
        context.Register(league, mapped);

        mapped.LeagueInstances = context.MapList(league.LeagueInstances, LeagueInstanceMapper.ToModel);

        return mapped;
    }
}
