using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class PlayerSeasonStatsMapper
{
    public static AppModels.PlayerSeasonStats? ToModel(DatabaseModels.PlayerSeasonStats? playerSeasonStats) =>
        ToModel(playerSeasonStats, new MappingContext());

    public static AppModels.PlayerSeasonStats? ToModel(DatabaseModels.PlayerSeasonStats? playerSeasonStats, MappingContext context)
    {
        if (playerSeasonStats is null)
        {
            return null;
        }

        if (context.TryGetMapped(playerSeasonStats, out AppModels.PlayerSeasonStats? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(playerSeasonStats, new AppModels.PlayerSeasonStats
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
