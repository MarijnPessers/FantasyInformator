using FantasyInformator.Database.Models.Scoring;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Context;

/// <summary>The tables of the Scoring group.</summary>
public static class ScoringSchema
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<FantasyScore>(fantasyScore =>
        {
            _ = fantasyScore.Property(x => x.Points).HasPrecision(ColumnSizes.ScorePrecision, ColumnSizes.ScoreScale);
            _ = fantasyScore.HasIndex(x => new { x.GameAppearanceId, x.LeagueInstanceId }).IsUnique();

            _ = fantasyScore.HasOne(fs => fs.GameAppearance)
                .WithMany(ga => ga.FantasyScores)
                .HasForeignKey(fs => fs.GameAppearanceId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = fantasyScore.HasOne(fs => fs.LeagueInstance)
                .WithMany(li => li.FantasyScores)
                .HasForeignKey(fs => fs.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        _ = modelBuilder.Entity<PlayerSeasonStats>(playerSeasonStats =>
        {
            _ = playerSeasonStats.Property(x => x.TotalScore).HasPrecision(
                ColumnSizes.ScorePrecision,
                ColumnSizes.ScoreScale);
            _ = playerSeasonStats.Property(x => x.AverageScore).HasPrecision(
                ColumnSizes.ScorePrecision,
                ColumnSizes.ScoreScale);
            _ = playerSeasonStats.Property(x => x.AverageScoreLast3).HasPrecision(
                ColumnSizes.ScorePrecision,
                ColumnSizes.ScoreScale);
            _ = playerSeasonStats.Property(x => x.MedianScore).HasPrecision(
                ColumnSizes.ScorePrecision,
                ColumnSizes.ScoreScale);
            _ = playerSeasonStats.HasIndex(x => new { x.PlayerId, x.LeagueInstanceId }).IsUnique();

            _ = playerSeasonStats.HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerSeasonStats)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = playerSeasonStats.HasOne(ps => ps.LeagueInstance)
                .WithMany(li => li.PlayerSeasonStats)
                .HasForeignKey(ps => ps.LeagueInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
