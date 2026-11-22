using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal sealed class NflTeamMapper : Mapper<NflTeam, NflTeamDb>
{
    protected override NflTeam CreateModel(NflTeamDb databaseModel) =>
        new()
        {
            ExternalId = databaseModel.ExternalId,
            Name = databaseModel.Name,
        };

    protected override NflTeamDb CreateDatabaseModel(NflTeam model) =>
        new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
        };
}
