using FantasyInformator.Domain;

namespace FantasyInformator.Database.Services;

/// <summary>What every model reads: all of its rows, without links to other models.</summary>
public interface IEntityService<TModel>
    where TModel : BaseEntity
{
    /// <summary>Gets all models. Links to other models are null, which means they are not retrieved.</summary>
    Task<IEnumerable<TModel>> GetAll(CancellationToken cancellationToken);
}

/// <summary>
/// The start of the read service of one model. <see cref="GetAll"/> works as it is. A model that needs more,
/// such as player positions, inherits this class, adds its own methods, and may override <see cref="GetAll"/>.
/// </summary>
internal abstract class EntityService<TModel>(IDatabaseReadService databaseReadService) : IEntityService<TModel>
    where TModel : BaseEntity
{
    public virtual Task<IEnumerable<TModel>> GetAll(CancellationToken cancellationToken) =>
        databaseReadService.ReadAll<TModel>(cancellationToken);
}
