using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

internal sealed class PlayerSeasonStatsMapper : EntityMapper<PlayerSeasonStats, PlayerSeasonStatsDb>
{
    protected override PlayerSeasonStats CreateCopy(PlayerSeasonStatsDb source) =>
        new()
        {
            PlayerId = source.PlayerId,
            LeagueInstanceId = source.LeagueInstanceId,
            TotalScore = source.TotalScore,
            GamesActive = source.GamesActive,
            AverageScore = source.AverageScore,
            AverageScoreLast3 = source.AverageScoreLast3,
            MedianScore = source.MedianScore,
        };

    protected override PlayerSeasonStatsDb CreateCopy(PlayerSeasonStats source) =>
        new()
        {
            PlayerId = source.PlayerId,
            LeagueInstanceId = source.LeagueInstanceId,
            TotalScore = source.TotalScore,
            GamesActive = source.GamesActive,
            AverageScore = source.AverageScore,
            AverageScoreLast3 = source.AverageScoreLast3,
            MedianScore = source.MedianScore,
        };
}
