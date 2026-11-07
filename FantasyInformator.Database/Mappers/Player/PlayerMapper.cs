using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PlayerMapper
{
    public static AppPlayer.Player? ToModel(DatabasePlayer.Player? player) =>
        ToModel(player, new MappingContext());

    public static AppPlayer.Player? ToModel(DatabasePlayer.Player? player, MappingContext context)
    {
        if (player is null)
        {
            return null;
        }

        if (context.TryGetMapped(player, out AppPlayer.Player? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(player, new AppPlayer.Player
        {
            ExternalId = player.ExternalId,
            Name = player.Name,
            CurrentNflTeamId = player.CurrentNflTeamId,
        });
        context.Register(player, mapped);

        mapped.CurrentNflTeam = NflTeamMapper.ToModel(player.CurrentNflTeam, context);
        mapped.PlayerPositions.AddRange(context.MapList(player.PlayerPositions, PlayerPositionMapper.ToModel));
        mapped.GameAppearances.AddRange(context.MapList(player.GameAppearances, GameAppearanceMapper.ToModel));
        mapped.RosteredPlayers.AddRange(context.MapList(player.RosteredPlayers, RosteredPlayerMapper.ToModel));
        mapped.PlayerSeasonStats.AddRange(context.MapList(player.PlayerSeasonStats, PlayerSeasonStatsMapper.ToModel));

        return mapped;
    }
}
