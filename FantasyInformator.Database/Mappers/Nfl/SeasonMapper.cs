using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal sealed class SeasonMapper : Mapper<Season, SeasonDb>
{
    protected override Season CreateModel(SeasonDb databaseModel) =>
        new()
        {
            Year = databaseModel.Year,
        };

    protected override SeasonDb CreateDatabaseModel(Season model) =>
        new()
        {
            Year = model.Year,
        };
}
