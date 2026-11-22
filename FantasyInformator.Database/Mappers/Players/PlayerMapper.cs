using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

public static class PlayerMapper
{
    public static Player Map(this PlayerDb player) =>
        new()
        {
            ExternalId = player.ExternalId,
            Name = player.Name,
            CurrentNflTeamId = player.CurrentNflTeamId,
            InjuryStatus = player.InjuryStatus,
            PlayerPositions = player.PlayerPositions?.Select(x => x.Map()).ToList(),
            GameAppearances = player.GameAppearances?.Select(x => x.Map()).ToList(),
            RosteredPlayers = player.RosteredPlayers?.Select(x => x.Map()).ToList(),
            PlayerSeasonStats = player.PlayerSeasonStats?.Select(x => x.Map()).ToList(),
        };

    public static IEnumerable<Player> Map(this IEnumerable<PlayerDb> players) =>
        players.Select(x => x.Map());

    public static PlayerDb Map(this Player player) =>
        new()
        {
            ExternalId = player.ExternalId,
            Name = player.Name,
            CurrentNflTeamId = player.CurrentNflTeamId,
            InjuryStatus = player.InjuryStatus,
            PlayerPositions = player.PlayerPositions?.Select(x => x.Map()).ToList(),
            GameAppearances = player.GameAppearances?.Select(x => x.Map()).ToList(),
            RosteredPlayers = player.RosteredPlayers?.Select(x => x.Map()).ToList(),
            PlayerSeasonStats = player.PlayerSeasonStats?.Select(x => x.Map()).ToList(),
        };

    public static IEnumerable<PlayerDb> Map(this IEnumerable<Player> players) =>
        players.Select(x => x.Map());
}
