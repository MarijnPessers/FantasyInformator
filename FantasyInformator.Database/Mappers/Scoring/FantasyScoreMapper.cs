using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Scoring;
using AppScoring = FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

public static class FantasyScoreMapper
{
    public static AppScoring.FantasyScore Map(this FantasyScoreDb fantasyScore) =>
        TrackedModelMapper.MapToAppModel(fantasyScore, new AppScoring.FantasyScore
        {
            GameAppearanceId = fantasyScore.GameAppearanceId,
            LeagueInstanceId = fantasyScore.LeagueInstanceId,
            Points = fantasyScore.Points,
        });

    public static FantasyScoreDb Map(this AppScoring.FantasyScore fantasyScore) =>
        TrackedModelMapper.Map(fantasyScore, new FantasyScoreDb
        {
            GameAppearanceId = fantasyScore.GameAppearanceId,
            LeagueInstanceId = fantasyScore.LeagueInstanceId,
            Points = fantasyScore.Points,
        });
}
