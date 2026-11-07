using FantasyInformator.Database.Models.FantasyTeam;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>
/// The tables of the FantasyTeam group. The owner of a team and the player on a roster are plain ids without a
/// foreign key, because they can point to an owner or player that is not known, so those navigations are not mapped.
/// </summary>
public static class FantasyTeamConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Owner>(owner =>
        {
            _ = owner.Property(x => x.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            _ = owner.Property(x => x.Name).HasMaxLength(ColumnSizes.Name);
            _ = owner.HasIndex(x => x.ExternalId).IsUnique();
            _ = owner.Ignore(x => x.FantasyTeams);
        });

        _ = modelBuilder.Entity<FantasyTeam>(fantasyTeam =>
        {
            _ = fantasyTeam.Property(x => x.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            _ = fantasyTeam.Property(x => x.Name).HasMaxLength(ColumnSizes.Name);
            _ = fantasyTeam.HasIndex(x => new { x.LeagueInstanceId, x.ExternalId }).IsUnique();
            _ = fantasyTeam.HasIndex(x => x.OwnerId);
            _ = fantasyTeam.Ignore(x => x.Owner);

            _ = fantasyTeam.HasOne(t => t.LeagueInstance)
                .WithMany(li => li.FantasyTeams)
                .HasForeignKey(t => t.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        _ = modelBuilder.Entity<RosteredPlayer>(rosteredPlayer =>
        {
            _ = rosteredPlayer.HasIndex(x => new { x.FantasyTeamId, x.PlayerId }).IsUnique();
            _ = rosteredPlayer.Ignore(x => x.Player);

            _ = rosteredPlayer.HasOne(rp => rp.FantasyTeam)
                .WithMany(t => t.RosteredPlayers)
                .HasForeignKey(rp => rp.FantasyTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = rosteredPlayer.HasOne(rp => rp.Slot)
                .WithMany(s => s.RosteredPlayers)
                .HasForeignKey(rp => rp.SlotId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
