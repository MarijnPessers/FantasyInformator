using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

internal static class LeagueMapper
{
    #region LeagueDb to League

    /// <summary>Turns a LeagueDb into a League. A new context fills in the links.</summary>
    public static League Map(this LeagueDb league) =>
        league.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static League Map(this LeagueDb league, MapContext context) =>
        context.Map(
            league,
            x => new League
            {
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of LeagueDb into League, all sharing one context.</summary>
    public static IEnumerable<League> Map(this IEnumerable<LeagueDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<League> Map(this IEnumerable<LeagueDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region League to LeagueDb

    /// <summary>Turns a League into a LeagueDb. A new context fills in the links.</summary>
    public static LeagueDb Map(this League league) =>
        league.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static LeagueDb Map(this League league, MapContext context) =>
        context.Map(
            league,
            x => new LeagueDb
            {
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of League into LeagueDb, all sharing one context.</summary>
    public static IEnumerable<LeagueDb> Map(this IEnumerable<League> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<LeagueDb> Map(this IEnumerable<League> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
