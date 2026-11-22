using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal sealed class OwnerMapper : Mapper<Owner, OwnerDb>
{
    protected override Owner CreateModel(OwnerDb databaseModel) =>
        new()
        {
            ExternalId = databaseModel.ExternalId,
            Name = databaseModel.Name,
        };

    protected override OwnerDb CreateDatabaseModel(Owner model) =>
        new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
        };
}
