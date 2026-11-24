using FantasyInformator.Database.Context;
using FantasyInformator.Database.Models;
using FantasyInformator.Domain;
using FantasyInformator.Domain.Enums;
using FantasyInformator.Shared.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FantasyInformator.Database.Services;

/// <summary>
/// Saves any model to this database. <see cref="BaseDatabaseWriteService"/> does the saving; this class gives it the
/// context, the database model of each model, the action for each model and the two dates of every database model.
/// The action comes from the state of the model: New is added, Pending is updated, Deleted is removed and Unchanged is
/// skipped.
/// Example: a list of 4 Players, one in each state, gives 1 added, 1 updated, 1 removed and 1 skipped.
/// </summary>
internal sealed class DatabaseWriteService(IDbContextFactory<FantasyInformatorDbContext> contextFactory)
    : BaseDatabaseWriteService
{
    /// <inheritdoc/>
    protected override async Task<DbContext> CreateContextAsync(CancellationToken cancellationToken) =>
        await contextFactory.CreateDbContextAsync(cancellationToken);

    /// <inheritdoc/>
    protected override Type FindDatabaseModel(Type modelType) =>
        DatabaseModelTypes.Find(modelType);

    /// <inheritdoc/>
    protected override SaveAction FindSaveAction(object model) => model switch
    {
        BaseEntity { State: TrackingState.New } => SaveAction.Add,
        BaseEntity { State: TrackingState.Pending } => SaveAction.Update,
        BaseEntity { State: TrackingState.Deleted } => SaveAction.Delete,
        _ => SaveAction.Skip,
    };

    /// <summary>An added database model is created and updated at the time of the save.</summary>
    protected override void OnAdded(EntityEntry entry, DateTime savedUtc)
    {
        entry.Property(nameof(BaseEntityDb.CreatedUtc)).CurrentValue = savedUtc;
        entry.Property(nameof(BaseEntityDb.UpdatedUtc)).CurrentValue = savedUtc;
    }

    /// <summary>An updated database model is updated at the time of the save and keeps its created time.</summary>
    protected override void OnUpdated(EntityEntry entry, DateTime savedUtc)
    {
        entry.Property(nameof(BaseEntityDb.UpdatedUtc)).CurrentValue = savedUtc;

        // An update writes every column, the created time too. The model sent in has no created time, so that column is
        // left out and the one in the database stays.
        // Example: a Player created on Monday and updated on Friday keeps Monday.
        entry.Property(nameof(BaseEntityDb.CreatedUtc)).IsModified = false;
    }
}
