using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;

namespace FantasyInformator.Shared.Mappers;

/// <summary>
/// Copying a model with its links (parent/child) would loop forever if not for this.
/// So this class remembers, within one mapping call, each original (the source) with its copy (the target). When a
/// link leads to an original that is already copied, it uses that copy. That stops the loop, and a link back points
/// to the same copy the call started from.
/// Which properties are links differs per project, so a class that derives from this one decides that in
/// <see cref="IsLink"/>.
/// Example: copying a team with 3 players gives a team whose 3 players all link back to that same team.
/// Use a new context for each mapping call, so a later call never reuses an old copy.
/// </summary>
public abstract class MapContext
{
    /// <summary>
    /// A list of links, one list per context class and pair of original and copy.
    /// <see cref="Map{TSource, TTarget}"/> is called many times for the same pair, and finding its links every time
    /// would be slow, because it reads every property of the copy. So the links of each pair are found once and kept
    /// here. The context class is part of the key, because each context class decides itself which properties are links.
    /// Example: for copying a saved team into a team, the link Players.
    /// </summary>
    private static readonly ConcurrentDictionary<(Type Context, Type Source, Type Target), IReadOnlyList<Link>> Links =
        new();

    /// <summary>
    /// <see cref="FindLinks"/> needs the mapper between the two classes of a link, but it only knows those classes
    /// while the program runs, and <see cref="MapperLookup.Find{TModel, TOtherModel}"/> needs them written in the code.
    /// So this information about that method is set to the two classes in <see cref="FindLinks"/>.
    /// Example: set to a saved player and a player, it finds the mapper between a player and a saved player.
    /// </summary>
    private static readonly MethodInfo FindMethodInfo =
        typeof(MapperLookup).GetMethod(nameof(MapperLookup.Find), 2, [typeof(Assembly)])!;

    /// <summary>
    /// Each original this mapping call has copied, kept with its copy.
    /// When a link leads to an original that was already copied, <see cref="Map{TSource, TTarget}"/> must give back that
    /// same copy instead of making a new one. So Map looks here first.
    /// An original is only found here when it is the very same object, not another object with the same values.
    /// Example: a saved team is copied into a team, so both are kept here. When a saved player links back to that saved
    /// team, Map finds it here and gives back the same team.
    /// </summary>
    private readonly Dictionary<object, object> _mapped = [with(ReferenceEqualityComparer.Instance)];

    /// <summary>
    /// Copies an original with all its links, or gives back its copy when this mapping call already copied it.
    /// Example: the second time the same saved team comes up, the team made the first time is given back.
    /// </summary>
    /// <param name="create">
    /// Makes the copy from the original, without its links. This method fills the links afterwards.
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
        OnCopied(source, target);

        // The copy is remembered before its links are filled. A link that leads back to this original, such as a
        // player's link to its team, then finds this copy instead of starting a new one.
        _mapped[source] = target;

        // The links of TSource and TTarget are taken from the field Links, or found with the method FindLinks when the
        // field has none yet. Then each link of the copy is filled.
        foreach (var link in Links.GetOrAdd(
            (GetType(), typeof(TSource), typeof(TTarget)),
            key => FindLinks(key.Source, key.Target)))
        {
            link.Fill(source, target, this);
        }

        return target;
    }

    /// <summary>
    /// True when a property that holds this type, or a list of it, is a link that is copied with its own mapper.
    /// </summary>
    protected abstract bool IsLink(Type type);

    /// <summary>
    /// Runs right after a copy is made, before its links are filled, for what every copy gets besides its own fields.
    /// Example: the Id of the original.
    /// </summary>
    protected virtual void OnCopied(object source, object target)
    {
        // By default every copy only gets its own fields.
    }

    /// <summary>
    /// To fill the links of a copy, we need to know which properties are links, and which mapper copies each one.
    /// A property of the copy is a link when <see cref="IsLink"/> is true for what it holds, or for what its list holds.
    /// The original has a property with the same name. The mapper that copies what it holds is found with
    /// <see cref="FindMethodInfo"/>, in the project of this context.
    /// Throws when the original has no property with that name.
    /// Example: a team's Players holds a list of players, so a saved team's Players is copied with the mapper between a
    /// saved player and a player.
    /// </summary>
    private List<Link> FindLinks(Type sourceType, Type targetType)
    {
        var links = new List<Link>();

        foreach (var targetProperty in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var isList = IsList(targetProperty.PropertyType);
            var targetLinkType = isList ? targetProperty.PropertyType.GetGenericArguments()[0] : targetProperty.PropertyType;

            if (!IsLink(targetLinkType))
            {
                continue;
            }

            var sourceProperty = sourceType.GetProperty(targetProperty.Name)
                ?? throw new MappingException(
                    $"{sourceType.Name} has no property {targetProperty.Name} to map into {targetType.Name}.",
                    sourceType,
                    targetType,
                    targetProperty.Name);
            var sourceLinkType = isList ? sourceProperty.PropertyType.GetGenericArguments()[0] : sourceProperty.PropertyType;

            var mapper = FindMethodInfo
                .MakeGenericMethod(sourceLinkType, targetLinkType)
                .CreateDelegate<Func<Assembly, IMapper>>()(GetType().Assembly);
            links.Add(new Link(sourceProperty, targetProperty, mapper, isList));
        }

        return links;
    }

    private static bool IsList(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);

    /// <summary>
    /// One link of a pair: the property of the original, the property of the copy, the mapper that copies what it
    /// holds, and whether it holds a list.
    /// Example: a saved player's Team, a player's Team, the mapper between a saved team and a team, and no list.
    /// </summary>
    private sealed record Link(PropertyInfo Source, PropertyInfo Target, IMapper Mapper, bool HoldsList)
    {
        /// <summary>
        /// Fills one link of the copy from the same link of the original. When the original's link is null, the copy's
        /// link stays null. For a list, a new empty list is set on the copy, and each object in the original's list is
        /// copied into it.
        /// Example: a saved team with 3 saved players gives a team with 3 players, each copied with the mapper between
        /// a saved player and a player.
        /// </summary>
        public void Fill(object source, object target, MapContext context)
        {
            var value = Source.GetValue(source);

            if (value is null)
            {
                return;
            }

            if (!HoldsList)
            {
                Target.SetValue(target, Mapper.Map(value, context));
                return;
            }

            var items = (IList)Activator.CreateInstance(Target.PropertyType)!;
            Target.SetValue(target, items);

            foreach (var item in (IList)value)
            {
                _ = items.Add(Mapper.Map(item, context));
            }
        }
    }
}
