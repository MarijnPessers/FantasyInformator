using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class PlayerMapper
{
    public static AppModels.Player? ToModel(DatabaseModels.Player? player) => ToModel(player, new MappingContext());

    public static AppModels.Player? ToModel(DatabaseModels.Player? player, MappingContext context)
    {
        if (player is null)
        {
            return null;
        }

        if (context.TryGetMapped(player, out AppModels.Player? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(player, new AppModels.Player
        {
            ExternalId = player.ExternalId,
            Name = player.Name,
            CurrentNflTeamId = player.CurrentNflTeamId,
        });
        context.Register(player, mapped);

        mapped.CurrentNflTeam = NflTeamMapper.ToModel(player.CurrentNflTeam, context);
        mapped.PlayerPositions = context.MapList(player.PlayerPositions, PlayerPositionMapper.ToModel);
        mapped.GameAppearances = context.MapList(player.GameAppearances, GameAppearanceMapper.ToModel);
        mapped.RosteredPlayers = context.MapList(player.RosteredPlayers, RosteredPlayerMapper.ToModel);
        mapped.PlayerSeasonStats = context.MapList(player.PlayerSeasonStats, PlayerSeasonStatsMapper.ToModel);

        return mapped;
    }
}
