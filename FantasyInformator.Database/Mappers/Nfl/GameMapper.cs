using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal sealed class GameMapper : Mapper<Game, GameDb>
{
    protected override Game CreateModel(GameDb databaseModel) =>
        new()
        {
            ExternalId = databaseModel.ExternalId,
            SeasonId = databaseModel.SeasonId,
            Week = databaseModel.Week,
            HomeNflTeamId = databaseModel.HomeNflTeamId,
            AwayNflTeamId = databaseModel.AwayNflTeamId,
            KickoffUtc = databaseModel.KickoffUtc,
        };

    protected override GameDb CreateDatabaseModel(Game model) =>
        new()
        {
            ExternalId = model.ExternalId,
            SeasonId = model.SeasonId,
            Week = model.Week,
            HomeNflTeamId = model.HomeNflTeamId,
            AwayNflTeamId = model.AwayNflTeamId,
            KickoffUtc = model.KickoffUtc,
        };
}
