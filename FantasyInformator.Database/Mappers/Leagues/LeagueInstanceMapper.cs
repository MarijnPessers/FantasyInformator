using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

internal static class LeagueInstanceMapper
{
    #region LeagueInstanceDb to LeagueInstance

    /// <summary>Turns a LeagueInstanceDb into a LeagueInstance. A new context fills in the links.</summary>
    public static LeagueInstance Map(this LeagueInstanceDb leagueInstance) =>
        leagueInstance.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static LeagueInstance Map(this LeagueInstanceDb leagueInstance, MapContext context) =>
        context.Map(
            leagueInstance,
            x => new LeagueInstance
            {
                ExternalId = x.ExternalId,
                LeagueId = x.LeagueId,
                SeasonId = x.SeasonId,
            }.MapBase(x));

    /// <summary>Turns a list of LeagueInstanceDb into LeagueInstance, all sharing one context.</summary>
    public static IEnumerable<LeagueInstance> Map(this IEnumerable<LeagueInstanceDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<LeagueInstance> Map(this IEnumerable<LeagueInstanceDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region LeagueInstance to LeagueInstanceDb

    /// <summary>Turns a LeagueInstance into a LeagueInstanceDb. A new context fills in the links.</summary>
    public static LeagueInstanceDb Map(this LeagueInstance leagueInstance) =>
        leagueInstance.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static LeagueInstanceDb Map(this LeagueInstance leagueInstance, MapContext context) =>
        context.Map(
            leagueInstance,
            x => new LeagueInstanceDb
            {
                ExternalId = x.ExternalId,
                LeagueId = x.LeagueId,
                SeasonId = x.SeasonId,
            }.MapBase(x));

    /// <summary>Turns a list of LeagueInstance into LeagueInstanceDb, all sharing one context.</summary>
    public static IEnumerable<LeagueInstanceDb> Map(this IEnumerable<LeagueInstance> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<LeagueInstanceDb> Map(this IEnumerable<LeagueInstance> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
