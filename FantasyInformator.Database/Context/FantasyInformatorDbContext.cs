using FantasyInformator.Database.Models.Administration;
using FantasyInformator.Database.Models.FantasyTeam;
using FantasyInformator.Database.Models.League;
using FantasyInformator.Database.Models.Lineup;
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Database.Models.Player;
using FantasyInformator.Database.Models.Scoring;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Context;

/// <summary>
/// The tables of the database. Which table has which settings is in this folder,
/// one <c>Schema</c> class per group.
/// </summary>
public class FantasyInformatorDbContext(DbContextOptions<FantasyInformatorDbContext> options) : DbContext(options)
{
    public DbSet<League> Leagues =>
        Set<League>();

    public DbSet<LeagueInstance> LeagueInstances =>
        Set<LeagueInstance>();

    public DbSet<Owner> Owners =>
        Set<Owner>();

    public DbSet<FantasyTeam> FantasyTeams =>
        Set<FantasyTeam>();

    public DbSet<RosteredPlayer> RosteredPlayers =>
        Set<RosteredPlayer>();

    public DbSet<Player> Players =>
        Set<Player>();

    public DbSet<Position> Positions =>
        Set<Position>();

    public DbSet<PlayerPosition> PlayerPositions =>
        Set<PlayerPosition>();

    public DbSet<GameAppearance> GameAppearances =>
        Set<GameAppearance>();

    public DbSet<Season> Seasons =>
        Set<Season>();

    public DbSet<NflTeam> NflTeams =>
        Set<NflTeam>();

    public DbSet<Game> Games =>
        Set<Game>();

    public DbSet<Slot> Slots =>
        Set<Slot>();

    public DbSet<LeagueSlot> LeagueSlots =>
        Set<LeagueSlot>();

    public DbSet<SlotEligibility> SlotEligibilities =>
        Set<SlotEligibility>();

    public DbSet<FantasyScore> FantasyScores =>
        Set<FantasyScore>();

    public DbSet<PlayerSeasonStats> PlayerSeasonStats =>
        Set<PlayerSeasonStats>();

    public DbSet<LastSleeperCall> LastSleeperCalls =>
        Set<LastSleeperCall>();

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
