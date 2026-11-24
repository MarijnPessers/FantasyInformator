using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

internal sealed class LeagueInstanceMapper : EntityMapper<LeagueInstance, LeagueInstanceDb>
{
    protected override LeagueInstance CreateCopy(LeagueInstanceDb source) =>
        new()
        {
            ExternalId = source.ExternalId,
            LeagueId = source.LeagueId,
            SeasonId = source.SeasonId,
        };

    protected override LeagueInstanceDb CreateCopy(LeagueInstance source) =>
        new()
        {
            ExternalId = source.ExternalId,
            LeagueId = source.LeagueId,
            SeasonId = source.SeasonId,
        };
}
