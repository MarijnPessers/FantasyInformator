using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

internal sealed class PlayerSeasonStatsMapper : Mapper<PlayerSeasonStats, PlayerSeasonStatsDb>
{
    protected override PlayerSeasonStats CreateModel(PlayerSeasonStatsDb databaseModel) =>
        new()
        {
            PlayerId = databaseModel.PlayerId,
            LeagueInstanceId = databaseModel.LeagueInstanceId,
            TotalScore = databaseModel.TotalScore,
            GamesActive = databaseModel.GamesActive,
            AverageScore = databaseModel.AverageScore,
            AverageScoreLast3 = databaseModel.AverageScoreLast3,
            MedianScore = databaseModel.MedianScore,
        };

    protected override PlayerSeasonStatsDb CreateDatabaseModel(PlayerSeasonStats model) =>
        new()
        {
            PlayerId = model.PlayerId,
            LeagueInstanceId = model.LeagueInstanceId,
            TotalScore = model.TotalScore,
            GamesActive = model.GamesActive,
            AverageScore = model.AverageScore,
            AverageScoreLast3 = model.AverageScoreLast3,
            MedianScore = model.MedianScore,
        };
}
