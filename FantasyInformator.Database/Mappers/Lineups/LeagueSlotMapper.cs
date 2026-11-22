using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal static class LeagueSlotMapper
{
    #region LeagueSlotDb to LeagueSlot

    /// <summary>Turns a LeagueSlotDb into a LeagueSlot. A new context fills in the links.</summary>
    public static LeagueSlot Map(this LeagueSlotDb leagueSlot) =>
        leagueSlot.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static LeagueSlot Map(this LeagueSlotDb leagueSlot, MapContext context) =>
        context.Map(
            leagueSlot,
            x => new LeagueSlot
            {
                LeagueInstanceId = x.LeagueInstanceId,
                SlotId = x.SlotId,
                Count = x.Count,
            }.MapBase(x));

    /// <summary>Turns a list of LeagueSlotDb into LeagueSlot, all sharing one context.</summary>
    public static IEnumerable<LeagueSlot> Map(this IEnumerable<LeagueSlotDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<LeagueSlot> Map(this IEnumerable<LeagueSlotDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region LeagueSlot to LeagueSlotDb

    /// <summary>Turns a LeagueSlot into a LeagueSlotDb. A new context fills in the links.</summary>
    public static LeagueSlotDb Map(this LeagueSlot leagueSlot) =>
        leagueSlot.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static LeagueSlotDb Map(this LeagueSlot leagueSlot, MapContext context) =>
        context.Map(
            leagueSlot,
            x => new LeagueSlotDb
            {
                LeagueInstanceId = x.LeagueInstanceId,
                SlotId = x.SlotId,
                Count = x.Count,
            }.MapBase(x));

    /// <summary>Turns a list of LeagueSlot into LeagueSlotDb, all sharing one context.</summary>
    public static IEnumerable<LeagueSlotDb> Map(this IEnumerable<LeagueSlot> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<LeagueSlotDb> Map(this IEnumerable<LeagueSlot> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
