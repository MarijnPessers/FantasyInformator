using FantasyInformator.Database.Context.Seeds;
using FantasyInformator.Database.Models.Players;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Context;

/// <summary>The tables of the Player group.</summary>
public static class PlayerSchema
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<PlayerDb>(player =>
        {
            _ = player.Property(x => x.ExternalId).HasMaxLength(ColumnSizes.ExternalId);
            _ = player.Property(x => x.Name).HasMaxLength(ColumnSizes.Name);
            _ = player.Property(x => x.InjuryStatus).HasConversion<string>().HasMaxLength(ColumnSizes.InjuryStatus);
            _ = player.HasIndex(x => x.ExternalId).IsUnique();
            _ = player.Ignore(x => x.RosteredPlayers);

            _ = player.HasOne(p => p.CurrentNflTeam)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.CurrentNflTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        _ = modelBuilder.Entity<PositionDb>(position =>
        {
            _ = position.Property(x => x.Name).HasMaxLength(ColumnSizes.Name);
            _ = position.Property(x => x.Abbreviation).HasMaxLength(ColumnSizes.Abbreviation);
            _ = position.HasIndex(x => x.Abbreviation).IsUnique();
            _ = position.HasData(PlayerSeed.Positions);
        });

        _ = modelBuilder.Entity<PlayerPositionDb>(playerPosition =>
        {
            _ = playerPosition.HasIndex(x => new { x.PlayerId, x.PositionId }).IsUnique();

            _ = playerPosition.HasOne(pp => pp.Player)
                .WithMany(p => p.PlayerPositions)
                .HasForeignKey(pp => pp.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = playerPosition.HasOne(pp => pp.Position)
                .WithMany(p => p.PlayerPositions)
                .HasForeignKey(pp => pp.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        _ = modelBuilder.Entity<GameAppearanceDb>(gameAppearance =>
        {
            _ = gameAppearance.HasIndex(x => new { x.PlayerId, x.GameId }).IsUnique();

            _ = gameAppearance.HasOne(ga => ga.Player)
                .WithMany(p => p.GameAppearances)
                .HasForeignKey(ga => ga.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = gameAppearance.HasOne(ga => ga.Game)
                .WithMany(g => g.GameAppearances)
                .HasForeignKey(ga => ga.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
