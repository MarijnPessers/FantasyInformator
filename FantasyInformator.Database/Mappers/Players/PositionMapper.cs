using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal static class PositionMapper
{
    #region PositionDb to Position

    /// <summary>Turns a PositionDb into a Position. A new context fills in the links.</summary>
    public static Position Map(this PositionDb position) =>
        position.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static Position Map(this PositionDb position, MapContext context) =>
        context.Map(
            position,
            x => new Position
            {
                Name = x.Name,
                Abbreviation = x.Abbreviation,
                IsDominant = x.IsDominant,
            }.MapBase(x));

    /// <summary>Turns a list of PositionDb into Position, all sharing one context.</summary>
    public static IEnumerable<Position> Map(this IEnumerable<PositionDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<Position> Map(this IEnumerable<PositionDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region Position to PositionDb

    /// <summary>Turns a Position into a PositionDb. A new context fills in the links.</summary>
    public static PositionDb Map(this Position position) =>
        position.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static PositionDb Map(this Position position, MapContext context) =>
        context.Map(
            position,
            x => new PositionDb
            {
                Name = x.Name,
                Abbreviation = x.Abbreviation,
                IsDominant = x.IsDominant,
            }.MapBase(x));

    /// <summary>Turns a list of Position into PositionDb, all sharing one context.</summary>
    public static IEnumerable<PositionDb> Map(this IEnumerable<Position> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<PositionDb> Map(this IEnumerable<Position> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
