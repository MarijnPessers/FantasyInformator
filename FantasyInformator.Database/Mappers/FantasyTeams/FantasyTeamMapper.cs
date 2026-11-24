using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal sealed class FantasyTeamMapper : EntityMapper<FantasyTeam, FantasyTeamDb>
{
    protected override FantasyTeam CreateCopy(FantasyTeamDb source) =>
        new()
        {
            ExternalId = source.ExternalId,
            OwnerId = source.OwnerId,
            LeagueInstanceId = source.LeagueInstanceId,
            Name = source.Name,
        };

    protected override FantasyTeamDb CreateCopy(FantasyTeam source) =>
        new()
        {
            ExternalId = source.ExternalId,
            OwnerId = source.OwnerId,
            LeagueInstanceId = source.LeagueInstanceId,
            Name = source.Name,
        };
}
