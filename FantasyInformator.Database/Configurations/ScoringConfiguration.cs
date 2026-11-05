using FantasyInformator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the Scoring group.</summary>
internal static class ScoringConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FantasyScore>(fantasyScore =>
        {
            fantasyScore.Property(fs => fs.Points).HasPrecision(ColumnSizes.ScorePrecision, ColumnSizes.ScoreScale);
            fantasyScore.HasIndex(fs => new { fs.GameAppearanceId, fs.LeagueInstanceId }).IsUnique();

            fantasyScore.HasOne(fs => fs.GameAppearance)
                .WithMany(ga => ga.FantasyScores)
                .HasForeignKey(fs => fs.GameAppearanceId)
                .OnDelete(DeleteBehavior.Cascade);

            fantasyScore.HasOne(fs => fs.LeagueInstance)
                .WithMany(li => li.FantasyScores)
                .HasForeignKey(fs => fs.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PlayerSeasonStats>(playerSeasonStats =>
        {
            playerSeasonStats.Property(ps => ps.TotalScore).HasPrecision(ColumnSizes.ScorePrecision, ColumnSizes.ScoreScale);
            playerSeasonStats.Property(ps => ps.AverageScore).HasPrecision(ColumnSizes.ScorePrecision, ColumnSizes.ScoreScale);
            playerSeasonStats.Property(ps => ps.AverageScoreLast3).HasPrecision(ColumnSizes.ScorePrecision, ColumnSizes.ScoreScale);
            playerSeasonStats.Property(ps => ps.MedianScore).HasPrecision(ColumnSizes.ScorePrecision, ColumnSizes.ScoreScale);
            playerSeasonStats.HasIndex(ps => new { ps.PlayerId, ps.LeagueInstanceId }).IsUnique();

            playerSeasonStats.HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerSeasonStats)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            playerSeasonStats.HasOne(ps => ps.LeagueInstance)
                .WithMany(li => li.PlayerSeasonStats)
                .HasForeignKey(ps => ps.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
