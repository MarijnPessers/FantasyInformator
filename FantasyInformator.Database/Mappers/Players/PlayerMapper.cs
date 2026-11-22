using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class PlayerMapper : Mapper<Player, PlayerDb>
{
    protected override Player CreateModel(PlayerDb databaseModel) =>
        new()
        {
            ExternalId = databaseModel.ExternalId,
            Name = databaseModel.Name,
            CurrentNflTeamId = databaseModel.CurrentNflTeamId,
            InjuryStatus = databaseModel.InjuryStatus,
        };

    protected override PlayerDb CreateDatabaseModel(Player model) =>
        new()
        {
            ExternalId = model.ExternalId,
            Name = model.Name,
            CurrentNflTeamId = model.CurrentNflTeamId,
            InjuryStatus = model.InjuryStatus,
        };
}
