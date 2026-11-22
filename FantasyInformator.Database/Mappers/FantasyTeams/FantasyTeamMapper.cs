using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.FantasyTeams;
using AppFantasyTeam = FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

public static class FantasyTeamMapper
{
    public static AppFantasyTeam.FantasyTeam Map(this FantasyTeamDb fantasyTeam) =>
        TrackedModelMapper.MapToAppModel(fantasyTeam, new AppFantasyTeam.FantasyTeam
        {
            ExternalId = fantasyTeam.ExternalId,
            OwnerId = fantasyTeam.OwnerId,
            LeagueInstanceId = fantasyTeam.LeagueInstanceId,
            Name = fantasyTeam.Name,
            RosteredPlayers = [.. fantasyTeam.RosteredPlayers.Select(x => x.Map())],
        });

    public static FantasyTeamDb Map(this AppFantasyTeam.FantasyTeam fantasyTeam) =>
        TrackedModelMapper.Map(fantasyTeam, new FantasyTeamDb
        {
            ExternalId = fantasyTeam.ExternalId,
            OwnerId = fantasyTeam.OwnerId,
            LeagueInstanceId = fantasyTeam.LeagueInstanceId,
            Name = fantasyTeam.Name,
            RosteredPlayers = [.. fantasyTeam.RosteredPlayers.Select(x => x.Map())],
        });
}
