using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Tracking;
using AppScoring = FantasyInformator.Models.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

public static class FantasyScoreMapper
{
    public static AppScoring.FantasyScore? ToModel(DatabaseScoring.FantasyScore? fantasyScore) =>
        ToModel(fantasyScore, new MappingContext());

    public static AppScoring.FantasyScore? ToModel(DatabaseScoring.FantasyScore? fantasyScore, MappingContext context)
    {
        if (fantasyScore is null)
        {
            return null;
        }

        if (context.TryGetMapped(fantasyScore, out AppScoring.FantasyScore? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(fantasyScore, new AppScoring.FantasyScore
        {
            GameAppearanceId = fantasyScore.GameAppearanceId,
            LeagueInstanceId = fantasyScore.LeagueInstanceId,
            Points = fantasyScore.Points,
        });
        context.Register(fantasyScore, mapped);

        mapped.GameAppearance = GameAppearanceMapper.ToModel(fantasyScore.GameAppearance, context);
        mapped.LeagueInstance = LeagueInstanceMapper.ToModel(fantasyScore.LeagueInstance, context);

        return mapped;
    }
}
