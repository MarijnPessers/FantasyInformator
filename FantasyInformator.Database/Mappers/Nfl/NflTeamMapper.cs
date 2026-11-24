using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal sealed class NflTeamMapper : EntityMapper<NflTeam, NflTeamDb>
{
    protected override NflTeam CreateCopy(NflTeamDb source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
        };

    protected override NflTeamDb CreateCopy(NflTeam source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
        };
}
