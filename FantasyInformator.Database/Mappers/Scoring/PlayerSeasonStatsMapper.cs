using FantasyInformator.Database.Models.Scoring;
using FantasyInformator.Domain.Scoring;

namespace FantasyInformator.Database.Mappers.Scoring;

internal static class PlayerSeasonStatsMapper
{
    #region PlayerSeasonStatsDb to PlayerSeasonStats

    /// <summary>Turns a PlayerSeasonStatsDb into a PlayerSeasonStats. A new context fills in the links.</summary>
    public static PlayerSeasonStats Map(this PlayerSeasonStatsDb playerSeasonStats) =>
        playerSeasonStats.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static PlayerSeasonStats Map(this PlayerSeasonStatsDb playerSeasonStats, MapContext context) =>
        context.Map(
            playerSeasonStats,
            x => new PlayerSeasonStats
            {
                PlayerId = x.PlayerId,
                LeagueInstanceId = x.LeagueInstanceId,
                TotalScore = x.TotalScore,
                GamesActive = x.GamesActive,
                AverageScore = x.AverageScore,
                AverageScoreLast3 = x.AverageScoreLast3,
                MedianScore = x.MedianScore,
            }.MapBase(x));

    /// <summary>Turns a list of PlayerSeasonStatsDb into PlayerSeasonStats, all sharing one context.</summary>
    public static IEnumerable<PlayerSeasonStats> Map(this IEnumerable<PlayerSeasonStatsDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<PlayerSeasonStats> Map(this IEnumerable<PlayerSeasonStatsDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region PlayerSeasonStats to PlayerSeasonStatsDb

    /// <summary>Turns a PlayerSeasonStats into a PlayerSeasonStatsDb. A new context fills in the links.</summary>
    public static PlayerSeasonStatsDb Map(this PlayerSeasonStats playerSeasonStats) =>
        playerSeasonStats.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static PlayerSeasonStatsDb Map(this PlayerSeasonStats playerSeasonStats, MapContext context) =>
        context.Map(
            playerSeasonStats,
            x => new PlayerSeasonStatsDb
            {
                PlayerId = x.PlayerId,
                LeagueInstanceId = x.LeagueInstanceId,
                TotalScore = x.TotalScore,
                GamesActive = x.GamesActive,
                AverageScore = x.AverageScore,
                AverageScoreLast3 = x.AverageScoreLast3,
                MedianScore = x.MedianScore,
            }.MapBase(x));

    /// <summary>Turns a list of PlayerSeasonStats into PlayerSeasonStatsDb, all sharing one context.</summary>
    public static IEnumerable<PlayerSeasonStatsDb> Map(this IEnumerable<PlayerSeasonStats> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<PlayerSeasonStatsDb> Map(this IEnumerable<PlayerSeasonStats> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
