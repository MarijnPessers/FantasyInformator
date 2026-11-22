using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

internal static class OwnerMapper
{
    #region OwnerDb to Owner

    /// <summary>Turns a OwnerDb into a Owner. A new context fills in the links.</summary>
    public static Owner Map(this OwnerDb owner) =>
        owner.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static Owner Map(this OwnerDb owner, MapContext context) =>
        context.Map(
            owner,
            x => new Owner
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of OwnerDb into Owner, all sharing one context.</summary>
    public static IEnumerable<Owner> Map(this IEnumerable<OwnerDb> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<Owner> Map(this IEnumerable<OwnerDb> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion

    #region Owner to OwnerDb

    /// <summary>Turns a Owner into a OwnerDb. A new context fills in the links.</summary>
    public static OwnerDb Map(this Owner owner) =>
        owner.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public static OwnerDb Map(this Owner owner, MapContext context) =>
        context.Map(
            owner,
            x => new OwnerDb
            {
                ExternalId = x.ExternalId,
                Name = x.Name,
            }.MapBase(x));

    /// <summary>Turns a list of Owner into OwnerDb, all sharing one context.</summary>
    public static IEnumerable<OwnerDb> Map(this IEnumerable<Owner> items) =>
        items.Map(new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public static IEnumerable<OwnerDb> Map(this IEnumerable<Owner> items, MapContext context) =>
        [.. items.Select(x => x.Map(context))];

    #endregion
}
