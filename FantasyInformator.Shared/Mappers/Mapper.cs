namespace FantasyInformator.Shared.Mappers;

/// <summary>
/// The Map methods of <see cref="Mapper{TModel, TOtherModel}"/> need the model or other model as its own type.
/// But some code only has it as an object, because its type is only found while the program runs. So this interface
/// copies an object with any mapper, without naming its type.
/// Example: a team that is only known as an object is copied into a saved team.
/// </summary>
public interface IMapper
{
    /// <summary>
    /// Copies a model into its other model, or an other model into its model, inside a mapping that is already
    /// running. Throws when this mapper does not copy that object.
    /// </summary>
    object Map(object source, MapContext context);
}

/// <summary>
/// Every pair of model and other model needs the same 8 Map methods: from other model to model and back, for one
/// object and for a list, each with and without a <see cref="MapContext"/>. Only copying the fields is different for
/// each pair. So this class holds the 8 Map methods, and a mapper class only writes the two CreateCopy methods and
/// <see cref="CreateContext"/>.
/// Example: a mapper between a team and a saved team only copies the name both ways.
/// </summary>
public abstract class Mapper<TModel, TOtherModel> : IMapper
    where TModel : class
    where TOtherModel : class
{
    /// <summary>Copies an other model into a model. A new context fills in the links.</summary>
    public TModel Map(TOtherModel otherModel) =>
        Map(otherModel, CreateContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public TModel Map(TOtherModel otherModel, MapContext context) =>
        context.Map(otherModel, CreateCopy);

    /// <summary>Copies a list of other models into models, all sharing one context.</summary>
    public IEnumerable<TModel> Map(IEnumerable<TOtherModel> otherModels) =>
        Map(otherModels, CreateContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public IEnumerable<TModel> Map(IEnumerable<TOtherModel> otherModels, MapContext context) =>
        [.. otherModels.Select(x => Map(x, context))];

    /// <summary>Copies a model into an other model. A new context fills in the links.</summary>
    public TOtherModel Map(TModel model) =>
        Map(model, CreateContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public TOtherModel Map(TModel model, MapContext context) =>
        context.Map(model, CreateCopy);

    /// <summary>Copies a list of models into other models, all sharing one context.</summary>
    public IEnumerable<TOtherModel> Map(IEnumerable<TModel> models) =>
        Map(models, CreateContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public IEnumerable<TOtherModel> Map(IEnumerable<TModel> models, MapContext context) =>
        [.. models.Select(x => Map(x, context))];

    /// <summary>
    /// Code that asks for a mapper between a saved team and a team can't use the mapper between a team and a saved
    /// team, because C# only accepts a mapper with its models in the order it was declared. So this gives the same
    /// mapper with the order turned around. It copies with the same CreateCopy methods and the same context.
    /// Example: the mapper between a team and a saved team, given as a mapper between a saved team and a team.
    /// </summary>
    public Mapper<TOtherModel, TModel> Reversed => field ??= new ReversedMapper(this);

    /// <inheritdoc/>
    object IMapper.Map(object source, MapContext context) => source switch
    {
        TOtherModel otherModel => Map(otherModel, context),
        TModel model => Map(model, context),
        _ => throw new MappingException(
            $"{GetType().Name} does not copy a {source.GetType().Name}.",
            source.GetType(),
            null),
    };

    /// <summary>
    /// Makes the context for a new mapping call. Which properties are links, and what every copy gets besides its
    /// fields, differs per project, so each project gives its own context.
    /// </summary>
    protected abstract MapContext CreateContext();

    /// <summary>Copies the fields of an other model into a new model, without the links.</summary>
    protected abstract TModel CreateCopy(TOtherModel source);

    /// <summary>Copies the fields of a model into a new other model, without the links.</summary>
    protected abstract TOtherModel CreateCopy(TModel source);

    /// <summary>
    /// The mapper of <see cref="Reversed"/>. Its CreateCopy methods and its context come from
    /// <paramref name="mapper"/>, so it copies exactly the same way.
    /// </summary>
    private sealed class ReversedMapper(Mapper<TModel, TOtherModel> mapper) : Mapper<TOtherModel, TModel>
    {
        protected override MapContext CreateContext() =>
            mapper.CreateContext();

        protected override TModel CreateCopy(TOtherModel source) =>
            mapper.CreateCopy(source);

        protected override TOtherModel CreateCopy(TModel source) =>
            mapper.CreateCopy(source);
    }
}
