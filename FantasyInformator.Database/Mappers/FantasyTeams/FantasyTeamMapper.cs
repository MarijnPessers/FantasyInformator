using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal static class FantasyTeamMapper
{
    #region FantasyTeamDb to FantasyTeam

    /// <summary>Turns a FantasyTeamDb into a FantasyTeam. A new context fills in the links.</summary>
    public static FantasyTeam Map(this FantasyTeamDb fantasyTeam) =>
        fantasyTeam.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static FantasyTeam Map(this FantasyTeamDb fantasyTeam, MapContext context) =>
        context.Map(
            fantasyTeam,
            x => new FantasyTeam
            {
                ExternalId = x.ExternalId,
                OwnerId = x.OwnerId,
                LeagueInstanceId = x.LeagueInstanceId,
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of FantasyTeamDb into FantasyTeam, all sharing one context.</summary>
    public static IEnumerable<FantasyTeam> Map(this IEnumerable<FantasyTeamDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<FantasyTeam> Map(this IEnumerable<FantasyTeamDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region FantasyTeam to FantasyTeamDb

    /// <summary>Turns a FantasyTeam into a FantasyTeamDb. A new context fills in the links.</summary>
    public static FantasyTeamDb Map(this FantasyTeam fantasyTeam) =>
        fantasyTeam.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static FantasyTeamDb Map(this FantasyTeam fantasyTeam, MapContext context) =>
        context.Map(
            fantasyTeam,
            x => new FantasyTeamDb
            {
                ExternalId = x.ExternalId,
                OwnerId = x.OwnerId,
                LeagueInstanceId = x.LeagueInstanceId,
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of FantasyTeam into FantasyTeamDb, all sharing one context.</summary>
    public static IEnumerable<FantasyTeamDb> Map(this IEnumerable<FantasyTeam> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<FantasyTeamDb> Map(this IEnumerable<FantasyTeam> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
