using FantasyInformator.Database.Mappers.Players;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Nfl;
using AppNfl = FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

public static class NflTeamMapper
{
    public static AppNfl.NflTeam Map(this NflTeamDb nflTeam) =>
        TrackedModelMapper.MapToAppModel(nflTeam, new AppNfl.NflTeam
        {
            ExternalId = nflTeam.ExternalId,
            Name = nflTeam.Name,
            Players = [.. nflTeam.Players.Select(x => x.Map())],
            HomeGames = [.. nflTeam.HomeGames.Select(x => x.Map())],
            AwayGames = [.. nflTeam.AwayGames.Select(x => x.Map())],
        });

    public static NflTeamDb Map(this AppNfl.NflTeam nflTeam) =>
        TrackedModelMapper.Map(nflTeam, new NflTeamDb
        {
            ExternalId = nflTeam.ExternalId,
            Name = nflTeam.Name,
            Players = [.. nflTeam.Players.Select(x => x.Map())],
            HomeGames = [.. nflTeam.HomeGames.Select(x => x.Map())],
            AwayGames = [.. nflTeam.AwayGames.Select(x => x.Map())],
        });
}
