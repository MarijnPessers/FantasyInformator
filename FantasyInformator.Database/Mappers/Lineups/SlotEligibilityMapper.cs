using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Domain.Lineups;

namespace FantasyInformator.Database.Mappers.Lineups;

internal static class SlotEligibilityMapper
{
    #region SlotEligibilityDb to SlotEligibility

    /// <summary>Turns a SlotEligibilityDb into a SlotEligibility. A new context fills in the links.</summary>
    public static SlotEligibility Map(this SlotEligibilityDb slotEligibility) =>
        slotEligibility.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static SlotEligibility Map(this SlotEligibilityDb slotEligibility, MapContext context) =>
        context.Map(
            slotEligibility,
            x => new SlotEligibility
            {
                PositionId = x.PositionId,
                SlotId = x.SlotId,
                IsPreferred = x.IsPreferred,
            }.MapBase(x));

    /// <summary>Turns a list of SlotEligibilityDb into SlotEligibility, all sharing one context.</summary>
    public static IEnumerable<SlotEligibility> Map(this IEnumerable<SlotEligibilityDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<SlotEligibility> Map(this IEnumerable<SlotEligibilityDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region SlotEligibility to SlotEligibilityDb

    /// <summary>Turns a SlotEligibility into a SlotEligibilityDb. A new context fills in the links.</summary>
    public static SlotEligibilityDb Map(this SlotEligibility slotEligibility) =>
        slotEligibility.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static SlotEligibilityDb Map(this SlotEligibility slotEligibility, MapContext context) =>
        context.Map(
            slotEligibility,
            x => new SlotEligibilityDb
            {
                PositionId = x.PositionId,
                SlotId = x.SlotId,
                IsPreferred = x.IsPreferred,
            }.MapBase(x));

    /// <summary>Turns a list of SlotEligibility into SlotEligibilityDb, all sharing one context.</summary>
    public static IEnumerable<SlotEligibilityDb> Map(this IEnumerable<SlotEligibility> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<SlotEligibilityDb> Map(this IEnumerable<SlotEligibility> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
