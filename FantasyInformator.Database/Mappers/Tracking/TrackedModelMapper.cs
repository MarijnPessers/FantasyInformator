using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Maps the fields every tracked model has, so no other mapper maps them.
/// </summary>
public static class TrackedModelMapper
{
    /// <summary>Copies <c>Id</c>, <c>State</c> and <c>UpdatedUtc</c> to the model and returns the model.</summary>
    public static TModel Fill<TModel>(DatabaseModels.TrackedModel databaseModel, TModel model)
        where TModel : AppModels.TrackedModel
    {
        model.Id = databaseModel.Id;
        model.State = Enum.Parse<AppModels.TrackingState>(databaseModel.State.ToString());
        model.UpdatedUtc = databaseModel.UpdatedUtc;
        return model;
    }
}
