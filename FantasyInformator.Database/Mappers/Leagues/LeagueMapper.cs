using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

internal sealed class LeagueMapper : EntityMapper<League, LeagueDb>
{
    protected override League CreateCopy(LeagueDb source) =>
        new()
        {
            Name = source.Name,
        };

    protected override LeagueDb CreateCopy(League source) =>
        new()
        {
            Name = source.Name,
        };
}
