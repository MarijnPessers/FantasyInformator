using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal sealed class RosteredPlayerMapper : Mapper<RosteredPlayer, RosteredPlayerDb>
{
    protected override RosteredPlayer CreateModel(RosteredPlayerDb databaseModel) =>
        new()
        {
            FantasyTeamId = databaseModel.FantasyTeamId,
            PlayerId = databaseModel.PlayerId,
            SlotId = databaseModel.SlotId,
        };

    protected override RosteredPlayerDb CreateDatabaseModel(RosteredPlayer model) =>
        new()
        {
            FantasyTeamId = model.FantasyTeamId,
            PlayerId = model.PlayerId,
            SlotId = model.SlotId,
        };
}
