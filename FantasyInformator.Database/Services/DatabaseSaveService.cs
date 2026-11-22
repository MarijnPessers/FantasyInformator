using FantasyInformator.Database.Context;
using FantasyInformator.Database.Models.Tracking;
using Microsoft.EntityFrameworkCore;
using AppEnums = FantasyInformator.Domain.Enums;
using AppTracking = FantasyInformator.Domain.Tracking;

namespace FantasyInformator.Database.Services;

internal interface IDatabaseSaveService
{
    /// <summary>
    /// Writes the changed models in one transaction: New models are inserted, Pending models are updated and
    /// Deleted models are deleted. Unchanged models are skipped. If anything fails, nothing is written.
    /// The given map turns each model into its database model.
    /// </summary>
    Task SaveAsync<TModel, TDatabaseModel>(
        IEnumerable<TModel> models,
        Func<TModel, TDatabaseModel> map,
        CancellationToken cancellationToken)
        where TModel : AppTracking.TrackedModel
        where TDatabaseModel : TrackedModelDb;
}

internal class DatabaseSaveService(IDbContextFactory<FantasyInformatorDbContext> contextFactory) : IDatabaseSaveService
{
    public async Task SaveAsync<TModel, TDatabaseModel>(
        IEnumerable<TModel> models,
        Func<TModel, TDatabaseModel> map,
        CancellationToken cancellationToken)
        where TModel : AppTracking.TrackedModel
        where TDatabaseModel : TrackedModelDb
    {
        var savedUtc = DateTime.UtcNow;
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        // Each changed model is in the list itself, so the children copied into a model must not be found and
        // written a second time through its links.
        context.ChangeTracker.AutoDetectChangesEnabled = false;

        foreach (var model in models)
        {
            Stage(context, model.State, () => MapWithId(model, map), savedUtc);
        }

        _ = await context.SaveChangesAsync(cancellationToken);
    }

    private static TDatabaseModel MapWithId<TModel, TDatabaseModel>(TModel model, Func<TModel, TDatabaseModel> map)
        where TModel : AppTracking.TrackedModel
        where TDatabaseModel : TrackedModelDb
    {
        var databaseModel = map(model);

        // The mappers never set the id.
        databaseModel.Id = model.Id;
        return databaseModel;
    }

    private static void Stage<TDatabaseModel>(
        FantasyInformatorDbContext context,
        AppEnums.TrackingState state,
        Func<TDatabaseModel> map,
        DateTime savedUtc)
        where TDatabaseModel : TrackedModelDb
    {
        switch (state)
        {
            case AppEnums.TrackingState.New:
                Insert(context, map(), savedUtc);
                break;
            case AppEnums.TrackingState.Pending:
                Update(context, map(), savedUtc);
                break;
            case AppEnums.TrackingState.Deleted:
                Delete(context, map());
                break;
            case AppEnums.TrackingState.Unchanged:
                break;
            default:
                // Unchanged models are never sent to the database.
                break;
        }
    }

    private static void Insert(
        FantasyInformatorDbContext context,
        TrackedModelDb databaseModel,
        DateTime savedUtc)
    {
        databaseModel.CreatedUtc = savedUtc;
        databaseModel.UpdatedUtc = savedUtc;
        context.Entry(databaseModel).State = EntityState.Added;
    }

    private static void Update(
        FantasyInformatorDbContext context,
        TrackedModelDb databaseModel,
        DateTime savedUtc)
    {
        databaseModel.UpdatedUtc = savedUtc;

        var entry = context.Entry(databaseModel);
        entry.State = EntityState.Modified;

        // The model has no created time, so the stored one must stay as it is.
        entry.Property(nameof(TrackedModelDb.CreatedUtc)).IsModified = false;
    }

    private static void Delete(FantasyInformatorDbContext context, TrackedModelDb databaseModel) =>
        context.Entry(databaseModel).State = EntityState.Deleted;
}
