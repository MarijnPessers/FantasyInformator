using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain.Nfl;

namespace FantasyInformator.Database.Mappers.Nfl;

internal static class NflTeamMapper
{
    #region NflTeamDb to NflTeam

    /// <summary>Turns a NflTeamDb into a NflTeam. A new context fills in the links.</summary>
    public static NflTeam Map(this NflTeamDb nflTeam) =>
        nflTeam.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static NflTeam Map(this NflTeamDb nflTeam, MapContext context) =>
        context.Map(
            nflTeam,
            x => new NflTeam
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of NflTeamDb into NflTeam, all sharing one context.</summary>
    public static IEnumerable<NflTeam> Map(this IEnumerable<NflTeamDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<NflTeam> Map(this IEnumerable<NflTeamDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region NflTeam to NflTeamDb

    /// <summary>Turns a NflTeam into a NflTeamDb. A new context fills in the links.</summary>
    public static NflTeamDb Map(this NflTeam nflTeam) =>
        nflTeam.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static NflTeamDb Map(this NflTeam nflTeam, MapContext context) =>
        context.Map(
            nflTeam,
            x => new NflTeamDb
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of NflTeam into NflTeamDb, all sharing one context.</summary>
    public static IEnumerable<NflTeamDb> Map(this IEnumerable<NflTeam> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<NflTeamDb> Map(this IEnumerable<NflTeam> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
