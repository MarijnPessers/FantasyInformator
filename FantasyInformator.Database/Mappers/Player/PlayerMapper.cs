using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PlayerMapper
{
    public static AppPlayer.Player? Map(this DatabasePlayer.Player? player) =>
        player.Map(new MappingContext());

    public static AppPlayer.Player? Map(this DatabasePlayer.Player? player, MappingContext context)
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

        mapped.CurrentNflTeam = player.CurrentNflTeam.Map(context);
        mapped.PlayerPositions.AddRange(
            context.MapList(player.PlayerPositions, PlayerPositionMapper.Map));
        mapped.GameAppearances.AddRange(
            context.MapList(player.GameAppearances, GameAppearanceMapper.Map));
        mapped.RosteredPlayers.AddRange(
            context.MapList(player.RosteredPlayers, RosteredPlayerMapper.Map));
        mapped.PlayerSeasonStats.AddRange(
            context.MapList(player.PlayerSeasonStats, PlayerSeasonStatsMapper.Map));

        return mapped;
    }

    public static DatabasePlayer.Player Map(this AppPlayer.Player player) =>
        TrackedModelMapper.FillDatabase(player, new DatabasePlayer.Player
        {
            ExternalId = player.ExternalId,
            Name = player.Name,
            CurrentNflTeamId = player.CurrentNflTeamId,
        });
}
