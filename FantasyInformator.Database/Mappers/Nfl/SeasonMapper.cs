using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal static class SeasonMapper
{
    #region SeasonDb to Season

    /// <summary>Turns a SeasonDb into a Season. A new context fills in the links.</summary>
    public static Season Map(this SeasonDb season) =>
        season.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static Season Map(this SeasonDb season, MapContext context) =>
        context.Map(
            season,
            x => new Season
            {
                Year = x.Year,
            }.MapBase(x));

    /// <summary>Turns a list of SeasonDb into Season, all sharing one context.</summary>
    public static IEnumerable<Season> Map(this IEnumerable<SeasonDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<Season> Map(this IEnumerable<SeasonDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region Season to SeasonDb

    /// <summary>Turns a Season into a SeasonDb. A new context fills in the links.</summary>
    public static SeasonDb Map(this Season season) =>
        season.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static SeasonDb Map(this Season season, MapContext context) =>
        context.Map(
            season,
            x => new SeasonDb
            {
                Year = x.Year,
            }.MapBase(x));

    /// <summary>Turns a list of Season into SeasonDb, all sharing one context.</summary>
    public static IEnumerable<SeasonDb> Map(this IEnumerable<Season> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<SeasonDb> Map(this IEnumerable<Season> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
