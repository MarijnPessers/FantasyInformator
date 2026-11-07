using AppEnums = FantasyInformator.Models.Enums;
using AppTracking = FantasyInformator.Models.Tracking;
using DatabaseTracking = FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Mappers.Tracking;

/// <summary>
/// Maps the fields every tracked model has, so no other mapper maps them.
/// </summary>
public static class TrackedModelMapper
{
    /// <summary>Copies <c>Id</c>, <c>State</c> and <c>UpdatedUtc</c> to the model and returns the model.</summary>
    public static TModel Fill<TModel>(DatabaseTracking.TrackedModel databaseModel, TModel model)
        where TModel : AppTracking.TrackedModel
    {
        model.Id = databaseModel.Id;
        model.State = Enum.Parse<AppEnums.TrackingState>(databaseModel.State.ToString());
        model.UpdatedUtc = databaseModel.UpdatedUtc;
        return model;
    }
}
