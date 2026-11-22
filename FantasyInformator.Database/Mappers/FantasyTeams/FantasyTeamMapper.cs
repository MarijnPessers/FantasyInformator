using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal sealed class FantasyTeamMapper : Mapper<FantasyTeam, FantasyTeamDb>
{
    protected override FantasyTeam CreateModel(FantasyTeamDb databaseModel) =>
        new()
        {
            ExternalId = databaseModel.ExternalId,
            OwnerId = databaseModel.OwnerId,
            LeagueInstanceId = databaseModel.LeagueInstanceId,
            Name = databaseModel.Name,
        };

    protected override FantasyTeamDb CreateDatabaseModel(FantasyTeam model) =>
        new()
        {
            ExternalId = model.ExternalId,
            OwnerId = model.OwnerId,
            LeagueInstanceId = model.LeagueInstanceId,
            Name = model.Name,
        };
}
