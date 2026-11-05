using FantasyInformator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>
/// The tables of the FantasyTeam group. The owner of a team and the player on a roster are plain ids without a
/// foreign key, because they can point to an owner or player that is not known, so those navigations are not mapped.
/// </summary>
internal static class FantasyTeamConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>(owner =>
        {
            owner.Property(o => o.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            owner.Property(o => o.Name).HasMaxLength(ColumnSizes.Name);
            owner.HasIndex(o => o.ExternalId).IsUnique();
            owner.Ignore(o => o.FantasyTeams);
        });

        modelBuilder.Entity<FantasyTeam>(fantasyTeam =>
        {
            fantasyTeam.Property(t => t.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            fantasyTeam.Property(t => t.Name).HasMaxLength(ColumnSizes.Name);
            fantasyTeam.HasIndex(t => new { t.LeagueInstanceId, t.ExternalId }).IsUnique();
            fantasyTeam.HasIndex(t => t.OwnerId);
            fantasyTeam.Ignore(t => t.Owner);

            fantasyTeam.HasOne(t => t.LeagueInstance)
                .WithMany(li => li.FantasyTeams)
                .HasForeignKey(t => t.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RosteredPlayer>(rosteredPlayer =>
        {
            rosteredPlayer.HasIndex(rp => new { rp.FantasyTeamId, rp.PlayerId }).IsUnique();
            rosteredPlayer.Ignore(rp => rp.Player);

            rosteredPlayer.HasOne(rp => rp.FantasyTeam)
                .WithMany(t => t.RosteredPlayers)
                .HasForeignKey(rp => rp.FantasyTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            rosteredPlayer.HasOne(rp => rp.Slot)
                .WithMany(s => s.RosteredPlayers)
                .HasForeignKey(rp => rp.SlotId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
