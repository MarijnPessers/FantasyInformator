using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal sealed class OwnerMapper : EntityMapper<Owner, OwnerDb>
{
    protected override Owner CreateCopy(OwnerDb source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
        };

    protected override OwnerDb CreateCopy(Owner source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
        };
}
