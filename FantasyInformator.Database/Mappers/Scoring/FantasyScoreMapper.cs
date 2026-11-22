using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

internal sealed class FantasyScoreMapper : Mapper<FantasyScore, FantasyScoreDb>
{
    protected override FantasyScore CreateModel(FantasyScoreDb databaseModel) =>
        new()
        {
            GameAppearanceId = databaseModel.GameAppearanceId,
            LeagueInstanceId = databaseModel.LeagueInstanceId,
            Points = databaseModel.Points,
        };

    protected override FantasyScoreDb CreateDatabaseModel(FantasyScore model) =>
        new()
        {
            GameAppearanceId = model.GameAppearanceId,
            LeagueInstanceId = model.LeagueInstanceId,
            Points = model.Points,
        };
}
