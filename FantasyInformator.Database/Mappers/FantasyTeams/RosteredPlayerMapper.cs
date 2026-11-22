using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal static class RosteredPlayerMapper
{
    #region RosteredPlayerDb to RosteredPlayer

    /// <summary>Turns a RosteredPlayerDb into a RosteredPlayer. A new context fills in the links.</summary>
    public static RosteredPlayer Map(this RosteredPlayerDb rosteredPlayer) =>
        rosteredPlayer.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static RosteredPlayer Map(this RosteredPlayerDb rosteredPlayer, MapContext context) =>
        context.Map(
            rosteredPlayer,
            x => new RosteredPlayer
            {
                FantasyTeamId = x.FantasyTeamId,
                PlayerId = x.PlayerId,
                SlotId = x.SlotId,
            }.MapBase(x));

    /// <summary>Turns a list of RosteredPlayerDb into RosteredPlayer, all sharing one context.</summary>
    public static IEnumerable<RosteredPlayer> Map(this IEnumerable<RosteredPlayerDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<RosteredPlayer> Map(this IEnumerable<RosteredPlayerDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region RosteredPlayer to RosteredPlayerDb

    /// <summary>Turns a RosteredPlayer into a RosteredPlayerDb. A new context fills in the links.</summary>
    public static RosteredPlayerDb Map(this RosteredPlayer rosteredPlayer) =>
        rosteredPlayer.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static RosteredPlayerDb Map(this RosteredPlayer rosteredPlayer, MapContext context) =>
        context.Map(
            rosteredPlayer,
            x => new RosteredPlayerDb
            {
                FantasyTeamId = x.FantasyTeamId,
                PlayerId = x.PlayerId,
                SlotId = x.SlotId,
            }.MapBase(x));

    /// <summary>Turns a list of RosteredPlayer into RosteredPlayerDb, all sharing one context.</summary>
    public static IEnumerable<RosteredPlayerDb> Map(this IEnumerable<RosteredPlayer> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<RosteredPlayerDb> Map(this IEnumerable<RosteredPlayer> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
