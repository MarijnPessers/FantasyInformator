using FantasyInformator.Database.Models;
using FantasyInformator.Domain;
using FantasyInformator.Shared.Mappers;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Every mapper between a model and its database model needs the same context, <see cref="EntityMapContext"/>. So this
/// class gives it, and a mapper class of this project derives from this one instead of from
/// <see cref="Mapper{TModel, TOtherModel}"/>.
/// Example: PlayerMapper derives from EntityMapper with Player and PlayerDb.
/// </summary>
internal abstract class EntityMapper<TModel, TDatabaseModel> : Mapper<TModel, TDatabaseModel>
    where TModel : BaseEntity
    where TDatabaseModel : BaseEntityDb
{
    /// <inheritdoc/>
    protected sealed override MapContext CreateContext() =>
        new EntityMapContext();
}
