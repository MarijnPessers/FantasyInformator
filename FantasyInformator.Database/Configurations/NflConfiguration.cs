using FantasyInformator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the Nfl group.</summary>
internal static class NflConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Season>(season =>
        {
            season.HasIndex(s => s.Year).IsUnique();
        });

        modelBuilder.Entity<NflTeam>(nflTeam =>
        {
            nflTeam.Property(t => t.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            nflTeam.Property(t => t.Name).HasMaxLength(ColumnSizes.Name);
            nflTeam.HasIndex(t => t.ExternalId).IsUnique();
        });

        modelBuilder.Entity<Game>(game =>
        {
            game.Property(g => g.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            game.HasIndex(g => g.ExternalId).IsUnique();
            game.HasIndex(g => new { g.SeasonId, g.Week });

            game.HasOne(g => g.Season)
                .WithMany(s => s.Games)
                .HasForeignKey(g => g.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            game.HasOne(g => g.HomeNflTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeNflTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            game.HasOne(g => g.AwayNflTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayNflTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
