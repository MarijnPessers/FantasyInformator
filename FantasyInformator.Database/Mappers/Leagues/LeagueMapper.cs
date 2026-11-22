using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

internal sealed class LeagueMapper : Mapper<League, LeagueDb>
{
    protected override League CreateModel(LeagueDb databaseModel) =>
        new()
        {
            Name = databaseModel.Name,
        };

    protected override LeagueDb CreateDatabaseModel(League model) =>
        new()
        {
            Name = model.Name,
        };
}
