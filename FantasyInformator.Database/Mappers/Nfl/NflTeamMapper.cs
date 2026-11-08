using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppNfl = FantasyInformator.Models.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class NflTeamMapper
{
    public static AppNfl.NflTeam? Map(this DatabaseNfl.NflTeam? nflTeam) =>
        nflTeam.Map(new MappingContext());

    public static AppNfl.NflTeam? Map(this DatabaseNfl.NflTeam? nflTeam, MappingContext context)
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

        mapped.Players.AddRange(context.MapList(nflTeam.Players, PlayerMapper.Map));
        mapped.HomeGames.AddRange(context.MapList(nflTeam.HomeGames, GameMapper.Map));
        mapped.AwayGames.AddRange(context.MapList(nflTeam.AwayGames, GameMapper.Map));

        return mapped;
    }

    public static DatabaseNfl.NflTeam Map(this AppNfl.NflTeam nflTeam) =>
        TrackedModelMapper.FillDatabase(nflTeam, new DatabaseNfl.NflTeam
        {
            ExternalId = nflTeam.ExternalId,
            Name = nflTeam.Name,
        });
}
