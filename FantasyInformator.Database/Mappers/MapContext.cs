using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using FantasyInformator.Database.Models;
using FantasyInformator.Domain;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Copying a model with its links (parent/child) would loop forever if not for this.
/// So this class remembers, within one mapping call, each original (the source) with its copy (the target). When a
/// link leads to an original that is already copied, it uses that copy. That stops the loop, and a link back points
/// to the same copy the call started from.
/// Example: copying an NflTeamDb with 3 PlayerDb into an NflTeam gives an NflTeam whose 3 Players all link back to
/// that same NflTeam.
/// Use a new MapContext for each mapping call, so a later call never reuses an old copy.
/// </summary>
internal sealed class MapContext
{
    /// <summary>
    /// A list of links, one list per pair of model and database model.
    /// <see cref="Map{TSource, TTarget}"/> is called many times for the same pair, and finding its links every time
    /// would be slow, because it reads every property of the model. So the links of each pair are found once and kept
    /// here.
    /// Example: for copying a PlayerDb into a Player, the links CurrentNflTeam, PlayerPositions, GameAppearances,
    /// RosteredPlayers and PlayerSeasonStats.
    /// </summary>
    private static readonly ConcurrentDictionary<(Type Source, Type Target), IReadOnlyList<Link>> Links = new();

    /// <summary>
    /// Each model or database model this mapping call has copied, kept with its copy.
    /// When a link leads to a model that was already copied, <see cref="Map{TSource, TTarget}"/> must give back that same
    /// copy instead of making a new one. So Map looks here first.
    /// A model is only found here when it is the very same object, not another object with the same values.
    /// Example: an NflTeamDb is copied into an NflTeam, so both are kept here. When a PlayerDb links back to that
    /// NflTeamDb, Map finds it here and gives back the same NflTeam.
    /// </summary>
    private readonly Dictionary<object, object> _mapped = new(ReferenceEqualityComparer.Instance);

    /// <summary>
    /// Copies model or database model with all its links, or gives back its copy when this mapping call already copied it.
    /// Example: the second time the same NflTeamDb comes up, the NflTeam made the first time is given back.
    /// </summary>
    /// <param name="create">
    /// Makes the copy from model or database model, without its links. This method fills the links afterwards.
    /// </param>
    public TTarget Map<TSource, TTarget>(TSource source, Func<TSource, TTarget> create)
        where TSource : class
        where TTarget : class
    {
        if (_mapped.TryGetValue(source, out var known))
        {
            return (TTarget)known;
        }

        var target = create(source);

        // The copy is remembered before its links are filled. A link that leads back to this original, such as a
        // player's link to its team, then finds this copy instead of starting a new one.
        _mapped[source] = target;

        // The links of TSource and TTarget are taken from the field Links, or found with the method FindLinks when the
        // field has none yet. Then each link of the copy is filled.
        foreach (var link in Links.GetOrAdd((typeof(TSource), typeof(TTarget)), key => FindLinks(key.Source, key.Target)))
        {
            link.Fill(source, target, this);
        }

        return target;
    }

    /// <summary>
    /// To fill the links of a copy, we need to know which properties are links, and which Map method copies each one.
    /// A property of the copy is a link when it holds a model or a list of models. The original has a property with the
    /// same name. The Map method that copies what it holds is found with <see cref="MapperLookup.Find"/>.
    /// Throws when the original has no property with that name.
    /// Example: Player's CurrentNflTeam holds an NflTeam, so PlayerDb's CurrentNflTeam is copied with the Map method
    /// that copies an NflTeamDb into an NflTeam.
    /// </summary>
    private static List<Link> FindLinks(Type sourceType, Type targetType)
    {
        var links = new List<Link>();

        foreach (var targetProperty in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var isList = IsList(targetProperty.PropertyType);
            var targetEntityType = isList ? targetProperty.PropertyType.GetGenericArguments()[0] : targetProperty.PropertyType;

            if (!IsEntity(targetEntityType))
            {
                continue;
            }

            var sourceProperty = sourceType.GetProperty(targetProperty.Name)
                ?? throw new InvalidOperationException(
                    $"{sourceType.Name} has no property {targetProperty.Name} to map into {targetType.Name}.");
            var sourceEntityType = isList ? sourceProperty.PropertyType.GetGenericArguments()[0] : sourceProperty.PropertyType;

            var mapEntity = MapperLookup.Find([sourceEntityType, typeof(MapContext)], targetEntityType);
            links.Add(new Link(sourceProperty, targetProperty, mapEntity, isList));
        }

        return links;
    }

    private static bool IsList(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);

    /// <summary>
    /// True for a model or a database model. A property that holds one, or a list of them, is a link.
    /// </summary>
    private static bool IsEntity(Type type) =>
        typeof(BaseEntity).IsAssignableFrom(type) || typeof(BaseEntityDb).IsAssignableFrom(type);

    /// <summary>
    /// One link of a pair: the property of the original, the property of the copy, the Map method that copies what it
    /// holds, and whether it holds a list.
    /// Example: PlayerDb's CurrentNflTeam, Player's CurrentNflTeam, the Map method that copies an NflTeamDb into an
    /// NflTeam, and no list.
    /// </summary>
    private sealed record Link(PropertyInfo Source, PropertyInfo Target, MethodInfo MapEntity, bool HoldsList)
    {
        /// <summary>
        /// Fills one link of the copy from the same link of the original. When the original's link is null, the copy's
        /// link stays null. For a list, a new empty list is set on the copy, and each model in the original's list is
        /// copied into it.
        /// Example: a PlayerDb with 3 PlayerPositions gives a Player with 3 PlayerPositions, each copied with the Map
        /// method that copies a PlayerPositionDb into a PlayerPosition.
        /// </summary>
        public void Fill(object source, object target, MapContext context)
        {
            var value = Source.GetValue(source);

            if (value is null)
            {
                return;
            }

            // The field MapEntity is only the information (MethodInfo) about the Map method, so the Map method runs
            // through Invoke.
            if (!HoldsList)
            {
                Target.SetValue(target, MapEntity.Invoke(null, [value, context]));
                return;
            }

            var items = (IList)Activator.CreateInstance(Target.PropertyType)!;
            Target.SetValue(target, items);

            foreach (var item in (IList)value)
            {
                _ = items.Add(MapEntity.Invoke(null, [item, context]));
            }
        }
    }
}
