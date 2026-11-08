using FantasyInformator.Database.Context.Seed;
using FantasyInformator.Database.Models.Nfl;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Context;

/// <summary>The tables of the Nfl group.</summary>
public static class NflSchema
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Season>(season =>
        {
            _ = season.HasIndex(x => x.Year).IsUnique();
        });

        _ = modelBuilder.Entity<NflTeam>(nflTeam =>
        {
            _ = nflTeam.Property(x => x.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            _ = nflTeam.Property(x => x.Name).HasMaxLength(ColumnSizes.Name);
            _ = nflTeam.HasIndex(x => x.ExternalId).IsUnique();
            _ = nflTeam.HasData(NflSeed.Teams);
        });

        _ = modelBuilder.Entity<Game>(game =>
        {
            _ = game.Property(x => x.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            _ = game.HasIndex(x => x.ExternalId).IsUnique();
            _ = game.HasIndex(x => new { x.SeasonId, x.Week });

            _ = game.HasOne(g => g.Season)
                .WithMany(s => s.Games)
                .HasForeignKey(g => g.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            _ = game.HasOne(g => g.HomeNflTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeNflTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            _ = game.HasOne(g => g.AwayNflTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayNflTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
