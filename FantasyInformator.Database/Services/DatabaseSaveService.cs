using FantasyInformator.Database.Context;
using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Mappers.Player;
using FantasyInformator.Database.Mappers.Scoring;
using Microsoft.EntityFrameworkCore;
using AppEnums = FantasyInformator.Models.Enums;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using AppLeague = FantasyInformator.Models.League;
using AppLineup = FantasyInformator.Models.Lineup;
using AppNfl = FantasyInformator.Models.Nfl;
using AppPlayer = FantasyInformator.Models.Player;
using AppScoring = FantasyInformator.Models.Scoring;
using AppTracking = FantasyInformator.Models.Tracking;
using DatabaseTracking = FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Services;

public interface IDatabaseSaveService
{
    /// <summary>
    /// Writes the changed models in one transaction: New models are inserted, Pending models are updated and
    /// Deleted models are deleted. Unchanged models are skipped. If anything fails, nothing is written.
    /// </summary>
    Task SaveAsync(IEnumerable<AppTracking.TrackedModel> models, CancellationToken cancellationToken);
}

public class DatabaseSaveService(
    IDbContextFactory<FantasyInformatorDbContext> contextFactory)
    : IDatabaseSaveService
{
    public async Task SaveAsync(IEnumerable<AppTracking.TrackedModel> models, CancellationToken cancellationToken)
    {
        var savedUtc = DateTime.UtcNow;
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        // Each changed model is in the list itself, so the children copied into a model must not be found and
        // written a second time through its links.
        context.ChangeTracker.AutoDetectChangesEnabled = false;

        foreach (var model in models)
        {
            Stage(context, model, savedUtc);
        }

        _ = await context.SaveChangesAsync(cancellationToken);
    }

    private static void Stage(FantasyInformatorDbContext context, AppTracking.TrackedModel model, DateTime savedUtc)
    {
        switch (model.State)
        {
            case AppEnums.TrackingState.New:
                Insert(context, model, savedUtc);
                break;
            case AppEnums.TrackingState.Pending:
                Update(context, model, savedUtc);
                break;
            case AppEnums.TrackingState.Deleted:
                Delete(context, model);
                break;
            case AppEnums.TrackingState.Unchanged:
                break;
            default:
                // Unchanged models are never sent to the database.
                break;
        }
    }

    private static void Insert(FantasyInformatorDbContext context, AppTracking.TrackedModel model, DateTime savedUtc)
    {
        var databaseModel = MapToDatabase(model);
        databaseModel.CreatedUtc = savedUtc;
        databaseModel.UpdatedUtc = savedUtc;
        context.Entry(databaseModel).State = EntityState.Added;
    }

    private static void Update(FantasyInformatorDbContext context, AppTracking.TrackedModel model, DateTime savedUtc)
    {
        var databaseModel = MapToDatabase(model);
        databaseModel.UpdatedUtc = savedUtc;

        var entry = context.Entry(databaseModel);
        entry.State = EntityState.Modified;

        // The model has no created time, so the stored one must stay as it is.
        entry.Property(nameof(DatabaseTracking.TrackedModel.CreatedUtc)).IsModified = false;
    }

    private static void Delete(FantasyInformatorDbContext context, AppTracking.TrackedModel model) =>
        context.Entry(MapToDatabase(model)).State = EntityState.Deleted;

    private static DatabaseTracking.TrackedModel MapToDatabase(AppTracking.TrackedModel model) =>
        model switch
        {
            AppLeague.League league => league.Map(),
            AppLeague.LeagueInstance leagueInstance => leagueInstance.Map(),
            AppFantasyTeam.Owner owner => owner.Map(),
            AppFantasyTeam.FantasyTeam fantasyTeam => fantasyTeam.Map(),
            AppFantasyTeam.RosteredPlayer rosteredPlayer => rosteredPlayer.Map(),
            AppPlayer.Player player => player.Map(),
            AppPlayer.Position position => position.Map(),
            AppPlayer.PlayerPosition playerPosition => playerPosition.Map(),
            AppPlayer.GameAppearance gameAppearance => gameAppearance.Map(),
            AppNfl.Season season => season.Map(),
            AppNfl.NflTeam nflTeam => nflTeam.Map(),
            AppNfl.Game game => game.Map(),
            AppLineup.Slot slot => slot.Map(),
            AppLineup.LeagueSlot leagueSlot => leagueSlot.Map(),
            AppLineup.SlotEligibility slotEligibility => slotEligibility.Map(),
            AppScoring.FantasyScore fantasyScore => fantasyScore.Map(),
            AppScoring.PlayerSeasonStats playerSeasonStats => playerSeasonStats.Map(),
            _ => throw new NotSupportedException(model.GetType().Name),
        };
}
