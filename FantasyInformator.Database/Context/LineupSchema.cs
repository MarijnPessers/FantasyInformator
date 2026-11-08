using FantasyInformator.Database.Context.Seed;
using FantasyInformator.Database.Models.Lineup;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Context;

/// <summary>The tables of the Lineup group.</summary>
public static class LineupSchema
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Slot>(slot =>
        {
            _ = slot.Property(x => x.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            _ = slot.Property(x => x.Name).HasMaxLength(ColumnSizes.Name);
            _ = slot.Property(x => x.Abbreviation).HasMaxLength(ColumnSizes.Abbreviation);
            _ = slot.HasIndex(x => x.ExternalId).IsUnique();
            _ = slot.HasData(LineupSeed.Slots);
        });

        _ = modelBuilder.Entity<LeagueSlot>(leagueSlot =>
        {
            _ = leagueSlot.HasIndex(x => new { x.LeagueInstanceId, x.SlotId }).IsUnique();

            _ = leagueSlot.HasOne(ls => ls.LeagueInstance)
                .WithMany(li => li.LeagueSlots)
                .HasForeignKey(ls => ls.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = leagueSlot.HasOne(ls => ls.Slot)
                .WithMany(s => s.LeagueSlots)
                .HasForeignKey(ls => ls.SlotId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        _ = modelBuilder.Entity<SlotEligibility>(slotEligibility =>
        {
            _ = slotEligibility.HasIndex(x => new { x.PositionId, x.SlotId }).IsUnique();

            _ = slotEligibility.HasOne(se => se.Position)
                .WithMany(p => p.SlotEligibilities)
                .HasForeignKey(se => se.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            _ = slotEligibility.HasOne(se => se.Slot)
                .WithMany(s => s.SlotEligibilities)
                .HasForeignKey(se => se.SlotId)
                .OnDelete(DeleteBehavior.Restrict);

            _ = slotEligibility.HasData(LineupSeed.Eligibilities);
        });
    }
}
