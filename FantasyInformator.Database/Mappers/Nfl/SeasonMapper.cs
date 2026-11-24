using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal sealed class SeasonMapper : EntityMapper<Season, SeasonDb>
{
    protected override Season CreateCopy(SeasonDb source) =>
        new()
        {
            Year = source.Year,
        };

    protected override SeasonDb CreateCopy(Season source) =>
        new()
        {
            Year = source.Year,
        };
}
