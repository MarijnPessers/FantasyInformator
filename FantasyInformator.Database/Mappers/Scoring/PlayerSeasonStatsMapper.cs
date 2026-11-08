using FantasyInformator.Database.Mappers.Tracking;
using AppScoring = FantasyInformator.Models.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

public static class PlayerSeasonStatsMapper
{
    public static AppScoring.PlayerSeasonStats Map(this DatabaseScoring.PlayerSeasonStats playerSeasonStats) =>
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

    public static DatabaseScoring.PlayerSeasonStats Map(this AppScoring.PlayerSeasonStats playerSeasonStats) =>
        TrackedModelMapper.MapToDbModel(playerSeasonStats, new DatabaseScoring.PlayerSeasonStats
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
