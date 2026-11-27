using FantasyInformator.Database.Models;
using FantasyInformator.Domain;
using FantasyInformator.Domain.Enums;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Maps the fields every entity has: the id and the state.
/// </summary>
internal static class BaseEntityMapper
{
    extension<TModel>(TModel model)
        where TModel : BaseEntity
    {
        /// <summary>
        /// Database model to model. The model comes from the database, so it matches the database and is
        /// Unchanged.
        /// </summary>
        public TModel MapBase(BaseEntityDb databaseModel)
        {
            model.Id = databaseModel.Id;
            model.State = TrackingState.Unchanged;
            return model;
        }
    }

    extension<TDatabaseModel>(TDatabaseModel databaseModel)
        where TDatabaseModel : BaseEntityDb
    {
        /// <summary>Model to database model. The database model has no state.</summary>
        public TDatabaseModel MapBase(BaseEntity model)
        {
            databaseModel.Id = model.Id;
            return databaseModel;
        }
    }
}
