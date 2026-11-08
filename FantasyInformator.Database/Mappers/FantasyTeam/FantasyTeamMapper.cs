using FantasyInformator.Database.Mappers.Tracking;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Mappers.FantasyTeam;

public static class FantasyTeamMapper
{
    public static AppFantasyTeam.FantasyTeam Map(this DatabaseFantasyTeam.FantasyTeam fantasyTeam) =>
        TrackedModelMapper.MapToAppModel(fantasyTeam, new AppFantasyTeam.FantasyTeam
        {
            ExternalId = fantasyTeam.ExternalId,
            OwnerId = fantasyTeam.OwnerId,
            LeagueInstanceId = fantasyTeam.LeagueInstanceId,
            Name = fantasyTeam.Name,
            RosteredPlayers = [.. fantasyTeam.RosteredPlayers.Select(x => x.Map())],
        });

    public static DatabaseFantasyTeam.FantasyTeam Map(this AppFantasyTeam.FantasyTeam fantasyTeam) =>
        TrackedModelMapper.MapToDbModel(fantasyTeam, new DatabaseFantasyTeam.FantasyTeam
        {
            ExternalId = fantasyTeam.ExternalId,
            OwnerId = fantasyTeam.OwnerId,
            LeagueInstanceId = fantasyTeam.LeagueInstanceId,
            Name = fantasyTeam.Name,
            RosteredPlayers = [.. fantasyTeam.RosteredPlayers.Select(x => x.Map())],
        });
}
