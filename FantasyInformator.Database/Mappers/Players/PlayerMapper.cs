using FantasyInformator.Database.Models.Players;
using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Mappers.Players;

internal static class PlayerMapper
{
    #region PlayerDb to Player

    /// <summary>Turns a PlayerDb into a Player. A new context fills in the links.</summary>
    public static Player Map(this PlayerDb player) =>
        player.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static Player Map(this PlayerDb player, MapContext context) =>
        context.Map(
            player,
            x => new Player
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
                CurrentNflTeamId = x.CurrentNflTeamId,
                InjuryStatus = x.InjuryStatus,
            }.MapBase(x));

    /// <summary>Turns a list of PlayerDb into Player, all sharing one context.</summary>
    public static IEnumerable<Player> Map(this IEnumerable<PlayerDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<Player> Map(this IEnumerable<PlayerDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region Player to PlayerDb

    /// <summary>Turns a Player into a PlayerDb. A new context fills in the links.</summary>
    public static PlayerDb Map(this Player player) =>
        player.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static PlayerDb Map(this Player player, MapContext context) =>
        context.Map(
            player,
            x => new PlayerDb
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
                CurrentNflTeamId = x.CurrentNflTeamId,
                InjuryStatus = x.InjuryStatus,
            }.MapBase(x));

    /// <summary>Turns a list of Player into PlayerDb, all sharing one context.</summary>
    public static IEnumerable<PlayerDb> Map(this IEnumerable<Player> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<PlayerDb> Map(this IEnumerable<Player> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
