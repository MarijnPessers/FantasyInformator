using FantasyInformator.Database.Models.League;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the League group.</summary>
public static class LeagueConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<League>(league =>
        {
            _ = league.Property(x => x.Name).HasMaxLength(ColumnSizes.Name);
        });

        _ = modelBuilder.Entity<LeagueInstance>(leagueInstance =>
        {
            _ = leagueInstance.Property(x => x.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            _ = leagueInstance.HasIndex(x => x.ExternalId).IsUnique();

            _ = leagueInstance.HasOne(li => li.League)
                .WithMany(l => l.LeagueInstances)
                .HasForeignKey(li => li.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = leagueInstance.HasOne(li => li.Season)
                .WithMany(s => s.LeagueInstances)
                .HasForeignKey(li => li.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
