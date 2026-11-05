using FantasyInformator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the Player group.</summary>
internal static class PlayerConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(player =>
        {
            player.Property(p => p.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            player.Property(p => p.Name).HasMaxLength(ColumnSizes.Name);
            player.HasIndex(p => p.ExternalId).IsUnique();
            player.Ignore(p => p.RosteredPlayers);

            player.HasOne(p => p.CurrentNflTeam)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.CurrentNflTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Position>(position =>
        {
            position.Property(p => p.Name).HasMaxLength(ColumnSizes.Name);
            position.Property(p => p.Abbreviation).HasMaxLength(ColumnSizes.Abbreviation);
            position.HasIndex(p => p.Abbreviation).IsUnique();
        });

        modelBuilder.Entity<PlayerPosition>(playerPosition =>
        {
            playerPosition.HasIndex(pp => new { pp.PlayerId, pp.PositionId }).IsUnique();

            playerPosition.HasOne(pp => pp.Player)
                .WithMany(p => p.PlayerPositions)
                .HasForeignKey(pp => pp.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            playerPosition.HasOne(pp => pp.Position)
                .WithMany(p => p.PlayerPositions)
                .HasForeignKey(pp => pp.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GameAppearance>(gameAppearance =>
        {
            gameAppearance.HasIndex(ga => new { ga.PlayerId, ga.GameId }).IsUnique();

            gameAppearance.HasOne(ga => ga.Player)
                .WithMany(p => p.GameAppearances)
                .HasForeignKey(ga => ga.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            gameAppearance.HasOne(ga => ga.Game)
                .WithMany(g => g.GameAppearances)
                .HasForeignKey(ga => ga.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
