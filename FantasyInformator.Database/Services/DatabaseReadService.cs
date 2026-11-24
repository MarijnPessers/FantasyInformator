using FantasyInformator.Database.Context;
using FantasyInformator.Database.Models;
using FantasyInformator.Shared.Database;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Services;

/// <summary>
/// Reads any model from this database. <see cref="BaseDatabaseReadService"/> does the reading; this class gives it the
/// context and the database model of each model.
/// Example: reading all Players reads every PlayerDb and returns them as Players.
/// </summary>
internal sealed class DatabaseReadService(IDbContextFactory<FantasyInformatorDbContext> contextFactory)
    : BaseDatabaseReadService
{
    /// <inheritdoc/>
    protected override async Task<DbContext> CreateContextAsync(CancellationToken cancellationToken) =>
        await contextFactory.CreateDbContextAsync(cancellationToken);

    /// <inheritdoc/>
    protected override Type FindDatabaseModel(Type modelType) =>
        DatabaseModelTypes.Find(modelType);
}
