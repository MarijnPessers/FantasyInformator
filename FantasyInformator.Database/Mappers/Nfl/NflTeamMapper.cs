using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppNfl = FantasyInformator.Models.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class NflTeamMapper
{
    public static AppNfl.NflTeam Map(this DatabaseNfl.NflTeam nflTeam) =>
        TrackedModelMapper.MapToAppModel(nflTeam, new AppNfl.NflTeam
        {
            ExternalId = nflTeam.ExternalId,
            Name = nflTeam.Name,
            Players = [.. nflTeam.Players.Select(x => x.Map())],
            HomeGames = [.. nflTeam.HomeGames.Select(x => x.Map())],
            AwayGames = [.. nflTeam.AwayGames.Select(x => x.Map())],
        });

    public static DatabaseNfl.NflTeam Map(this AppNfl.NflTeam nflTeam) =>
        TrackedModelMapper.MapToDbModel(nflTeam, new DatabaseNfl.NflTeam
        {
            ExternalId = nflTeam.ExternalId,
            Name = nflTeam.Name,
            Players = [.. nflTeam.Players.Select(x => x.Map())],
            HomeGames = [.. nflTeam.HomeGames.Select(x => x.Map())],
            AwayGames = [.. nflTeam.AwayGames.Select(x => x.Map())],
        });
}
