using FantasyInformator.Database.Models;
using FantasyInformator.Domain;
using FantasyInformator.Domain.Enums;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Maps the fields every entity has: the id and the state.
/// </summary>
internal static class BaseEntityMapper
{
    /// <summary>
    /// Database model to model. The model comes from the database, so it matches the database and is Unchanged.
    /// </summary>
    public static TModel MapBase<TModel>(this TModel model, BaseEntityDb databaseModel)
        where TModel : BaseEntity
    {
        model.Id = databaseModel.Id;
        model.State = TrackingState.Unchanged;
        return model;
    }

    /// <summary>Model to database model. The database model has no state.</summary>
    public static TDatabaseModel MapBase<TDatabaseModel>(this TDatabaseModel databaseModel, BaseEntity model)
        where TDatabaseModel : BaseEntityDb
    {
        databaseModel.Id = model.Id;
        return databaseModel;
    }
}
