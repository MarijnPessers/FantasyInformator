using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class GameAppearanceMapper : Mapper<GameAppearance, GameAppearanceDb>
{
    protected override GameAppearance CreateModel(GameAppearanceDb databaseModel) =>
        new()
        {
            PlayerId = databaseModel.PlayerId,
            GameId = databaseModel.GameId,
            IsHome = databaseModel.IsHome,
        };

    protected override GameAppearanceDb CreateDatabaseModel(GameAppearance model) =>
        new()
        {
            PlayerId = model.PlayerId,
            GameId = model.GameId,
            IsHome = model.IsHome,
        };
}
