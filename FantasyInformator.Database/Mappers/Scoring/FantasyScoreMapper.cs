using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

internal static class FantasyScoreMapper
{
    #region FantasyScoreDb to FantasyScore

    /// <summary>Turns a FantasyScoreDb into a FantasyScore. A new context fills in the links.</summary>
    public static FantasyScore Map(this FantasyScoreDb fantasyScore) =>
        fantasyScore.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static FantasyScore Map(this FantasyScoreDb fantasyScore, MapContext context) =>
        context.Map(
            fantasyScore,
            x => new FantasyScore
            {
                GameAppearanceId = x.GameAppearanceId,
                LeagueInstanceId = x.LeagueInstanceId,
                Points = x.Points,
            }.MapBase(x));

    /// <summary>Turns a list of FantasyScoreDb into FantasyScore, all sharing one context.</summary>
    public static IEnumerable<FantasyScore> Map(this IEnumerable<FantasyScoreDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<FantasyScore> Map(this IEnumerable<FantasyScoreDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region FantasyScore to FantasyScoreDb

    /// <summary>Turns a FantasyScore into a FantasyScoreDb. A new context fills in the links.</summary>
    public static FantasyScoreDb Map(this FantasyScore fantasyScore) =>
        fantasyScore.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static FantasyScoreDb Map(this FantasyScore fantasyScore, MapContext context) =>
        context.Map(
            fantasyScore,
            x => new FantasyScoreDb
            {
                GameAppearanceId = x.GameAppearanceId,
                LeagueInstanceId = x.LeagueInstanceId,
                Points = x.Points,
            }.MapBase(x));

    /// <summary>Turns a list of FantasyScore into FantasyScoreDb, all sharing one context.</summary>
    public static IEnumerable<FantasyScoreDb> Map(this IEnumerable<FantasyScore> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<FantasyScoreDb> Map(this IEnumerable<FantasyScore> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
