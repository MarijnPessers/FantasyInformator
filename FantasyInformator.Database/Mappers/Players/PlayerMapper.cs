using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class PlayerMapper : EntityMapper<Player, PlayerDb>
{
    protected override Player CreateCopy(PlayerDb source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
            CurrentNflTeamId = source.CurrentNflTeamId,
            InjuryStatus = source.InjuryStatus,
        };

    protected override PlayerDb CreateCopy(Player source) =>
        new()
        {
            ExternalId = source.ExternalId,
            Name = source.Name,
            CurrentNflTeamId = source.CurrentNflTeamId,
            InjuryStatus = source.InjuryStatus,
        };
}
