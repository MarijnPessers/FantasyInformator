using AppEnums = FantasyInformator.Models.Enums;
using AppTracking = FantasyInformator.Models.Tracking;
using DatabaseTracking = FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Mappers.Tracking;

/// <summary>
/// Maps the fields every tracked model has, so no other mapper maps them.
/// </summary>
public static class TrackedModelMapper
{
    /// <summary>
    /// Copies <c>Id</c> to the model and sets its state to Unchanged, because a record read from the database is
    /// unchanged. The dates of the database model stay in the database. Returns the model.
    /// </summary>
    public static TModel Fill<TModel>(DatabaseTracking.TrackedModel databaseModel, TModel model)
        where TModel : AppTracking.TrackedModel
    {
        model.Id = databaseModel.Id;
        model.State = AppEnums.TrackingState.Unchanged;
        return model;
    }

    /// <summary>
    /// Copies <c>Id</c> to the database model and returns it. The dates are set when the model is saved.
    /// </summary>
    public static TDatabaseModel FillDatabase<TDatabaseModel>(
        AppTracking.TrackedModel model,
        TDatabaseModel databaseModel)
        where TDatabaseModel : DatabaseTracking.TrackedModel
    {
        databaseModel.Id = model.Id;
        return databaseModel;
    }
}
