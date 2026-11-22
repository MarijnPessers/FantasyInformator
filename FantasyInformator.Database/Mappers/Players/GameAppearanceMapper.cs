using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal static class GameAppearanceMapper
{
    #region GameAppearanceDb to GameAppearance

    /// <summary>Turns a GameAppearanceDb into a GameAppearance. A new context fills in the links.</summary>
    public static GameAppearance Map(this GameAppearanceDb gameAppearance) =>
        gameAppearance.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static GameAppearance Map(this GameAppearanceDb gameAppearance, MapContext context) =>
        context.Map(
            gameAppearance,
            x => new GameAppearance
            {
                PlayerId = x.PlayerId,
                GameId = x.GameId,
                IsHome = x.IsHome,
            }.MapBase(x));

    /// <summary>Turns a list of GameAppearanceDb into GameAppearance, all sharing one context.</summary>
    public static IEnumerable<GameAppearance> Map(this IEnumerable<GameAppearanceDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<GameAppearance> Map(this IEnumerable<GameAppearanceDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region GameAppearance to GameAppearanceDb

    /// <summary>Turns a GameAppearance into a GameAppearanceDb. A new context fills in the links.</summary>
    public static GameAppearanceDb Map(this GameAppearance gameAppearance) =>
        gameAppearance.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static GameAppearanceDb Map(this GameAppearance gameAppearance, MapContext context) =>
        context.Map(
            gameAppearance,
            x => new GameAppearanceDb
            {
                PlayerId = x.PlayerId,
                GameId = x.GameId,
                IsHome = x.IsHome,
            }.MapBase(x));

    /// <summary>Turns a list of GameAppearance into GameAppearanceDb, all sharing one context.</summary>
    public static IEnumerable<GameAppearanceDb> Map(this IEnumerable<GameAppearance> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<GameAppearanceDb> Map(this IEnumerable<GameAppearance> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
