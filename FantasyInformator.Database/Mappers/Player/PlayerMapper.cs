using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using AppPlayer = FantasyInformator.Models.Player;
using DatabasePlayer = FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Mappers.Player;

public static class PlayerMapper
{
    public static AppPlayer.Player Map(this DatabasePlayer.Player player) =>
        TrackedModelMapper.MapToAppModel(player, new AppPlayer.Player
        {
            ExternalId = player.ExternalId,
            Name = player.Name,
            CurrentNflTeamId = player.CurrentNflTeamId,
            PlayerPositions = [.. player.PlayerPositions.Select(x => x.Map())],
            GameAppearances = [.. player.GameAppearances.Select(x => x.Map())],
            RosteredPlayers = [.. player.RosteredPlayers.Select(x => x.Map())],
            PlayerSeasonStats = [.. player.PlayerSeasonStats.Select(x => x.Map())],
        });

    public static DatabasePlayer.Player Map(this AppPlayer.Player player) =>
        TrackedModelMapper.MapToDbModel(player, new DatabasePlayer.Player
        {
            ExternalId = player.ExternalId,
            Name = player.Name,
            CurrentNflTeamId = player.CurrentNflTeamId,
            PlayerPositions = [.. player.PlayerPositions.Select(x => x.Map())],
            GameAppearances = [.. player.GameAppearances.Select(x => x.Map())],
            RosteredPlayers = [.. player.RosteredPlayers.Select(x => x.Map())],
            PlayerSeasonStats = [.. player.PlayerSeasonStats.Select(x => x.Map())],
        });
}
