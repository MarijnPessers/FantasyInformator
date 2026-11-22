using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

internal sealed class LeagueInstanceMapper : Mapper<LeagueInstance, LeagueInstanceDb>
{
    protected override LeagueInstance CreateModel(LeagueInstanceDb databaseModel) =>
        new()
        {
            ExternalId = databaseModel.ExternalId,
            LeagueId = databaseModel.LeagueId,
            SeasonId = databaseModel.SeasonId,
        };

    protected override LeagueInstanceDb CreateDatabaseModel(LeagueInstance model) =>
        new()
        {
            ExternalId = model.ExternalId,
            LeagueId = model.LeagueId,
            SeasonId = model.SeasonId,
        };
}
