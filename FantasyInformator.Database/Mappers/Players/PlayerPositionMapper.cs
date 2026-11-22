using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal static class PlayerPositionMapper
{
    #region PlayerPositionDb to PlayerPosition

    /// <summary>Turns a PlayerPositionDb into a PlayerPosition. A new context fills in the links.</summary>
    public static PlayerPosition Map(this PlayerPositionDb playerPosition) =>
        playerPosition.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static PlayerPosition Map(this PlayerPositionDb playerPosition, MapContext context) =>
        context.Map(
            playerPosition,
            x => new PlayerPosition
            {
                PlayerId = x.PlayerId,
                PositionId = x.PositionId,
            }.MapBase(x));

    /// <summary>Turns a list of PlayerPositionDb into PlayerPosition, all sharing one context.</summary>
    public static IEnumerable<PlayerPosition> Map(this IEnumerable<PlayerPositionDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<PlayerPosition> Map(this IEnumerable<PlayerPositionDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region PlayerPosition to PlayerPositionDb

    /// <summary>Turns a PlayerPosition into a PlayerPositionDb. A new context fills in the links.</summary>
    public static PlayerPositionDb Map(this PlayerPosition playerPosition) =>
        playerPosition.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static PlayerPositionDb Map(this PlayerPosition playerPosition, MapContext context) =>
        context.Map(
            playerPosition,
            x => new PlayerPositionDb
            {
                PlayerId = x.PlayerId,
                PositionId = x.PositionId,
            }.MapBase(x));

    /// <summary>Turns a list of PlayerPosition into PlayerPositionDb, all sharing one context.</summary>
    public static IEnumerable<PlayerPositionDb> Map(this IEnumerable<PlayerPosition> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<PlayerPositionDb> Map(this IEnumerable<PlayerPosition> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
