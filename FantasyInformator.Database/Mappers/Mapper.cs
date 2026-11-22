using FantasyInformator.Database.Models;
using FantasyInformator.Domain;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Every pair of model and database model needs the same 8 Map methods: from database model to model and back, for
/// one model and for a list, each with and without a <see cref="MapContext"/>. Only copying the fields is different for
/// each pair. So this class holds the 8 Map methods, and a mapper class only writes <see cref="CreateModel"/> and
/// <see cref="CreateDatabaseModel"/>. The Map methods copy the Id and set the state themselves.
/// Example: PlayerMapper only copies ExternalId, Name, CurrentNflTeamId and InjuryStatus between a PlayerDb and a
/// Player.
/// </summary>
internal abstract class Mapper<TModel, TDatabaseModel> : IMapper
    where TModel : BaseEntity
    where TDatabaseModel : BaseEntityDb
{
    /// <summary>Copies a database model into a model. A new context fills in the links.</summary>
    public TModel Map(TDatabaseModel databaseModel) =>
        Map(databaseModel, new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public TModel Map(TDatabaseModel databaseModel, MapContext context) =>
        context.Map(databaseModel, x => CreateModel(x).MapBase(x));

    /// <summary>Copies a list of database models into models, all sharing one context.</summary>
    public IEnumerable<TModel> Map(IEnumerable<TDatabaseModel> databaseModels) =>
        Map(databaseModels, new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public IEnumerable<TModel> Map(IEnumerable<TDatabaseModel> databaseModels, MapContext context) =>
        [.. databaseModels.Select(x => Map(x, context))];

    /// <summary>Copies a model into a database model. A new context fills in the links.</summary>
    public TDatabaseModel Map(TModel model) =>
        Map(model, new MapContext());

    /// <summary>The same, inside a mapping that is already running, so objects met twice are found.</summary>
    public TDatabaseModel Map(TModel model, MapContext context) =>
        context.Map(model, x => CreateDatabaseModel(x).MapBase(x));

    /// <summary>Copies a list of models into database models, all sharing one context.</summary>
    public IEnumerable<TDatabaseModel> Map(IEnumerable<TModel> models) =>
        Map(models, new MapContext());

    /// <summary>The same, inside a mapping that is already running.</summary>
    public IEnumerable<TDatabaseModel> Map(IEnumerable<TModel> models, MapContext context) =>
        [.. models.Select(x => Map(x, context))];

    /// <inheritdoc/>
    object IMapper.Map(object source, MapContext context) => source switch
    {
        TDatabaseModel databaseModel => Map(databaseModel, context),
        TModel model => Map(model, context),
        _ => throw new ArgumentException($"{GetType().Name} does not copy a {source.GetType().Name}.", nameof(source)),
    };

    /// <summary>Copies the fields of a database model into a new model, without the Id, the state and the links.</summary>
    protected abstract TModel CreateModel(TDatabaseModel databaseModel);

    /// <summary>Copies the fields of a model into a new database model, without the Id and the links.</summary>
    protected abstract TDatabaseModel CreateDatabaseModel(TModel model);
}
