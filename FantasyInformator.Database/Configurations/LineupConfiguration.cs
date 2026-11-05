using FantasyInformator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the Lineup group.</summary>
internal static class LineupConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Slot>(slot =>
        {
            slot.Property(s => s.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            slot.Property(s => s.Name).HasMaxLength(ColumnSizes.Name);
            slot.Property(s => s.Abbreviation).HasMaxLength(ColumnSizes.Abbreviation);
            slot.HasIndex(s => s.ExternalId).IsUnique();
        });

        modelBuilder.Entity<LeagueSlot>(leagueSlot =>
        {
            leagueSlot.HasIndex(ls => new { ls.LeagueInstanceId, ls.SlotId }).IsUnique();

            leagueSlot.HasOne(ls => ls.LeagueInstance)
                .WithMany(li => li.LeagueSlots)
                .HasForeignKey(ls => ls.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            leagueSlot.HasOne(ls => ls.Slot)
                .WithMany(s => s.LeagueSlots)
                .HasForeignKey(ls => ls.SlotId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SlotEligibility>(slotEligibility =>
        {
            slotEligibility.HasIndex(se => new { se.PositionId, se.SlotId }).IsUnique();

            slotEligibility.HasOne(se => se.Position)
                .WithMany(p => p.SlotEligibilities)
                .HasForeignKey(se => se.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            slotEligibility.HasOne(se => se.Slot)
                .WithMany(s => s.SlotEligibilities)
                .HasForeignKey(se => se.SlotId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
