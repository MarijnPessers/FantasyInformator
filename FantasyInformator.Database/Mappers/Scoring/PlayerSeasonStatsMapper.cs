using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppScoring = FantasyInformator.Models.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

public static class PlayerSeasonStatsMapper
{
    public static AppScoring.PlayerSeasonStats? Map(
        this DatabaseScoring.PlayerSeasonStats? playerSeasonStats) =>
        playerSeasonStats.Map(new MappingContext());

    public static AppScoring.PlayerSeasonStats? Map(
        this DatabaseScoring.PlayerSeasonStats? playerSeasonStats,
        MappingContext context)
    {
        if (playerSeasonStats is null)
        {
            return null;
        }

        if (context.TryGetMapped(playerSeasonStats, out AppScoring.PlayerSeasonStats? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(playerSeasonStats, new AppScoring.PlayerSeasonStats
        {
            PlayerId = playerSeasonStats.PlayerId,
            LeagueInstanceId = playerSeasonStats.LeagueInstanceId,
            TotalScore = playerSeasonStats.TotalScore,
            GamesActive = playerSeasonStats.GamesActive,
            AverageScore = playerSeasonStats.AverageScore,
            AverageScoreLast3 = playerSeasonStats.AverageScoreLast3,
            MedianScore = playerSeasonStats.MedianScore,
        });
        context.Register(playerSeasonStats, mapped);

        mapped.Player = playerSeasonStats.Player.Map(context);
        mapped.LeagueInstance = playerSeasonStats.LeagueInstance.Map(context);

        return mapped;
    }

    public static DatabaseScoring.PlayerSeasonStats Map(this AppScoring.PlayerSeasonStats playerSeasonStats) =>
        TrackedModelMapper.FillDatabase(playerSeasonStats, new DatabaseScoring.PlayerSeasonStats
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
