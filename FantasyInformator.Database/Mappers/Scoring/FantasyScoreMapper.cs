using FantasyInformator.Database.Mappers.Tracking;
using AppScoring = FantasyInformator.Models.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

public static class FantasyScoreMapper
{
    public static AppScoring.FantasyScore Map(this DatabaseScoring.FantasyScore fantasyScore) =>
        TrackedModelMapper.MapToAppModel(fantasyScore, new AppScoring.FantasyScore
        {
            GameAppearanceId = fantasyScore.GameAppearanceId,
            LeagueInstanceId = fantasyScore.LeagueInstanceId,
            Points = fantasyScore.Points,
        });

    public static DatabaseScoring.FantasyScore Map(this AppScoring.FantasyScore fantasyScore) =>
        TrackedModelMapper.MapToDbModel(fantasyScore, new DatabaseScoring.FantasyScore
        {
            GameAppearanceId = fantasyScore.GameAppearanceId,
            LeagueInstanceId = fantasyScore.LeagueInstanceId,
            Points = fantasyScore.Points,
        });
}
