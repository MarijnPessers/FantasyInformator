using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Scoring;
using AppScoring = FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

public static class PlayerSeasonStatsMapper
{
    public static AppScoring.PlayerSeasonStats Map(this PlayerSeasonStatsDb playerSeasonStats) =>
        TrackedModelMapper.MapToAppModel(playerSeasonStats, new AppScoring.PlayerSeasonStats
        {
            PlayerId = playerSeasonStats.PlayerId,
            LeagueInstanceId = playerSeasonStats.LeagueInstanceId,
            TotalScore = playerSeasonStats.TotalScore,
            GamesActive = playerSeasonStats.GamesActive,
            AverageScore = playerSeasonStats.AverageScore,
            AverageScoreLast3 = playerSeasonStats.AverageScoreLast3,
            MedianScore = playerSeasonStats.MedianScore,
        });

    public static PlayerSeasonStatsDb Map(this AppScoring.PlayerSeasonStats playerSeasonStats) =>
        TrackedModelMapper.Map(playerSeasonStats, new PlayerSeasonStatsDb
        {
            PlayerId = playerSeasonStats.PlayerId,
            LeagueInstanceId = playerSeasonStats.LeagueInstanceId,
            TotalScore = playerSeasonStats.TotalScore,
            GamesActive = playerSeasonStats.GamesActive,
            AverageScore = playerSeasonStats.AverageScore,
            AverageScoreLast3 = playerSeasonStats.AverageScoreLast3,
            MedianScore = playerSeasonStats.MedianScore,
        });
}
