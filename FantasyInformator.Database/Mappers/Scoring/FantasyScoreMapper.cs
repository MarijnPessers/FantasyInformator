using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

internal sealed class FantasyScoreMapper : EntityMapper<FantasyScore, FantasyScoreDb>
{
    protected override FantasyScore CreateCopy(FantasyScoreDb source) =>
        new()
        {
            GameAppearanceId = source.GameAppearanceId,
            LeagueInstanceId = source.LeagueInstanceId,
            Points = source.Points,
        };

    protected override FantasyScoreDb CreateCopy(FantasyScore source) =>
        new()
        {
            GameAppearanceId = source.GameAppearanceId,
            LeagueInstanceId = source.LeagueInstanceId,
            Points = source.Points,
        };
}
