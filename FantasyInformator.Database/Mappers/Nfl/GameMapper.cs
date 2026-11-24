using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal sealed class GameMapper : EntityMapper<Game, GameDb>
{
    protected override Game CreateCopy(GameDb source) =>
        new()
        {
            ExternalId = source.ExternalId,
            SeasonId = source.SeasonId,
            Week = source.Week,
            HomeNflTeamId = source.HomeNflTeamId,
            AwayNflTeamId = source.AwayNflTeamId,
            KickoffUtc = source.KickoffUtc,
        };

    protected override GameDb CreateCopy(Game source) =>
        new()
        {
            ExternalId = source.ExternalId,
            SeasonId = source.SeasonId,
            Week = source.Week,
            HomeNflTeamId = source.HomeNflTeamId,
            AwayNflTeamId = source.AwayNflTeamId,
            KickoffUtc = source.KickoffUtc,
        };
}
