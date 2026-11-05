using FantasyInformator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the League group.</summary>
internal static class LeagueConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<League>(league =>
        {
            league.Property(l => l.Name).HasMaxLength(ColumnSizes.Name);
        });

        modelBuilder.Entity<LeagueInstance>(leagueInstance =>
        {
            leagueInstance.Property(li => li.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            leagueInstance.HasIndex(li => li.ExternalId).IsUnique();

            leagueInstance.HasOne(li => li.League)
                .WithMany(l => l.LeagueInstances)
                .HasForeignKey(li => li.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

            leagueInstance.HasOne(li => li.Season)
                .WithMany(s => s.LeagueInstances)
                .HasForeignKey(li => li.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
