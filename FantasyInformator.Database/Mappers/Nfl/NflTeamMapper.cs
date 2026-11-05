using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class NflTeamMapper
{
    public static AppModels.NflTeam? ToModel(DatabaseModels.NflTeam? nflTeam) => ToModel(nflTeam, new MappingContext());

    public static AppModels.NflTeam? ToModel(DatabaseModels.NflTeam? nflTeam, MappingContext context)
    {
        if (nflTeam is null)
        {
            return null;
        }

        if (context.TryGetMapped(nflTeam, out AppModels.NflTeam? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(nflTeam, new AppModels.NflTeam
        {
            ExternalId = nflTeam.ExternalId,
            Name = nflTeam.Name,
        });
        context.Register(nflTeam, mapped);

        mapped.Players = context.MapList(nflTeam.Players, PlayerMapper.ToModel);
        mapped.HomeGames = context.MapList(nflTeam.HomeGames, GameMapper.ToModel);
        mapped.AwayGames = context.MapList(nflTeam.AwayGames, GameMapper.ToModel);

        return mapped;
    }
}
