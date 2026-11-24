using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal sealed class GameAppearanceMapper : EntityMapper<GameAppearance, GameAppearanceDb>
{
    protected override GameAppearance CreateCopy(GameAppearanceDb source) =>
        new()
        {
            PlayerId = source.PlayerId,
            GameId = source.GameId,
            IsHome = source.IsHome,
        };

    protected override GameAppearanceDb CreateCopy(GameAppearance source) =>
        new()
        {
            PlayerId = source.PlayerId,
            GameId = source.GameId,
            IsHome = source.IsHome,
        };
}
