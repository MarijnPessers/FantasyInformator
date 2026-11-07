using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppNfl = FantasyInformator.Models.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class NflTeamMapper
{
    public static AppNfl.NflTeam? ToModel(DatabaseNfl.NflTeam? nflTeam) =>
        ToModel(nflTeam, new MappingContext());

    public static AppNfl.NflTeam? ToModel(DatabaseNfl.NflTeam? nflTeam, MappingContext context)
    {
        if (nflTeam is null)
        {
            return null;
        }

        if (context.TryGetMapped(nflTeam, out AppNfl.NflTeam? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(nflTeam, new AppNfl.NflTeam
        {
            ExternalId = nflTeam.ExternalId,
            Name = nflTeam.Name,
        });
        context.Register(nflTeam, mapped);

        mapped.Players.AddRange(context.MapList(nflTeam.Players, PlayerMapper.ToModel));
        mapped.HomeGames.AddRange(context.MapList(nflTeam.HomeGames, GameMapper.ToModel));
        mapped.AwayGames.AddRange(context.MapList(nflTeam.AwayGames, GameMapper.ToModel));

        return mapped;
    }
}
