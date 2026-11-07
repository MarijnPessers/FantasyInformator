using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppScoring = FantasyInformator.Models.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

public static class PlayerSeasonStatsMapper
{
    public static AppScoring.PlayerSeasonStats? ToModel(DatabaseScoring.PlayerSeasonStats? playerSeasonStats) =>
        ToModel(playerSeasonStats, new MappingContext());

    public static AppScoring.PlayerSeasonStats? ToModel(
        DatabaseScoring.PlayerSeasonStats? playerSeasonStats,
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

        mapped.Player = PlayerMapper.ToModel(playerSeasonStats.Player, context);
        mapped.LeagueInstance = LeagueInstanceMapper.ToModel(playerSeasonStats.LeagueInstance, context);

        return mapped;
    }
}
