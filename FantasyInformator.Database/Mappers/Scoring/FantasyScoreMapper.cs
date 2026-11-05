using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class FantasyScoreMapper
{
    public static AppModels.FantasyScore? ToModel(DatabaseModels.FantasyScore? fantasyScore) =>
        ToModel(fantasyScore, new MappingContext());

    public static AppModels.FantasyScore? ToModel(DatabaseModels.FantasyScore? fantasyScore, MappingContext context)
    {
        if (fantasyScore is null)
        {
            return null;
        }

        if (context.TryGetMapped(fantasyScore, out AppModels.FantasyScore? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(fantasyScore, new AppModels.FantasyScore
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
