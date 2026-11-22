using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal static class SlotMapper
{
    #region SlotDb to Slot

    /// <summary>Turns a SlotDb into a Slot. A new context fills in the links.</summary>
    public static Slot Map(this SlotDb slot) =>
        slot.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static Slot Map(this SlotDb slot, MapContext context) =>
        context.Map(
            slot,
            x => new Slot
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
                Abbreviation = x.Abbreviation,
            }.MapBase(x));

    /// <summary>Turns a list of SlotDb into Slot, all sharing one context.</summary>
    public static IEnumerable<Slot> Map(this IEnumerable<SlotDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<Slot> Map(this IEnumerable<SlotDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region Slot to SlotDb

    /// <summary>Turns a Slot into a SlotDb. A new context fills in the links.</summary>
    public static SlotDb Map(this Slot slot) =>
        slot.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static SlotDb Map(this Slot slot, MapContext context) =>
        context.Map(
            slot,
            x => new SlotDb
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
                Abbreviation = x.Abbreviation,
            }.MapBase(x));

    /// <summary>Turns a list of Slot into SlotDb, all sharing one context.</summary>
    public static IEnumerable<SlotDb> Map(this IEnumerable<Slot> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<SlotDb> Map(this IEnumerable<Slot> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
