using FantasyInformator.Database.Models.Administration;
using FantasyInformator.Database.Models.FantasyTeams;
using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Database.Models.Players;
using FantasyInformator.Database.Models.Scoring;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Context;

/// <summary>
/// The tables of the database. Which table has which settings is in this folder,
/// one <c>Schema</c> class per group.
/// </summary>
public class FantasyInformatorDbContext(DbContextOptions<FantasyInformatorDbContext> options) : DbContext(options)
{
    public DbSet<LeagueDb> Leagues =>
        Set<LeagueDb>();

    public DbSet<LeagueInstanceDb> LeagueInstances =>
        Set<LeagueInstanceDb>();

    public DbSet<OwnerDb> Owners =>
        Set<OwnerDb>();

    public DbSet<FantasyTeamDb> FantasyTeams =>
        Set<FantasyTeamDb>();

    public DbSet<RosteredPlayerDb> RosteredPlayers =>
        Set<RosteredPlayerDb>();

    public DbSet<PlayerDb> Players =>
        Set<PlayerDb>();

    public DbSet<PositionDb> Positions =>
        Set<PositionDb>();

    public DbSet<PlayerPositionDb> PlayerPositions =>
        Set<PlayerPositionDb>();

    public DbSet<GameAppearanceDb> GameAppearances =>
        Set<GameAppearanceDb>();

    public DbSet<SeasonDb> Seasons =>
        Set<SeasonDb>();

    public DbSet<NflTeamDb> NflTeams =>
        Set<NflTeamDb>();

    public DbSet<GameDb> Games =>
        Set<GameDb>();

    public DbSet<SlotDb> Slots =>
        Set<SlotDb>();

    public DbSet<LeagueSlotDb> LeagueSlots =>
        Set<LeagueSlotDb>();

    public DbSet<SlotEligibilityDb> SlotEligibilities =>
        Set<SlotEligibilityDb>();

    public DbSet<FantasyScoreDb> FantasyScores =>
        Set<FantasyScoreDb>();

    public DbSet<PlayerSeasonStatsDb> PlayerSeasonStats =>
        Set<PlayerSeasonStatsDb>();

    public DbSet<LastSleeperCallDb> LastSleeperCalls =>
        Set<LastSleeperCallDb>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        LeagueSchema.Configure(modelBuilder);
        FantasyTeamSchema.Configure(modelBuilder);
        PlayerSchema.Configure(modelBuilder);
        NflSchema.Configure(modelBuilder);
        LineupSchema.Configure(modelBuilder);
        ScoringSchema.Configure(modelBuilder);
        AdministrationSchema.Configure(modelBuilder);
    }
}
