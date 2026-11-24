using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal sealed class RosteredPlayerMapper : EntityMapper<RosteredPlayer, RosteredPlayerDb>
{
    protected override RosteredPlayer CreateCopy(RosteredPlayerDb source) =>
        new()
        {
            FantasyTeamId = source.FantasyTeamId,
            PlayerId = source.PlayerId,
            SlotId = source.SlotId,
        };

    protected override RosteredPlayerDb CreateCopy(RosteredPlayer source) =>
        new()
        {
            FantasyTeamId = source.FantasyTeamId,
            PlayerId = source.PlayerId,
            SlotId = source.SlotId,
        };
}
