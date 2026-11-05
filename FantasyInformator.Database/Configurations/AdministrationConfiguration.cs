using FantasyInformator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the Administration group.</summary>
internal static class AdministrationConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LastSleeperCall>(lastSleeperCall =>
        {
            lastSleeperCall.HasKey(c => c.CallName);
            lastSleeperCall.Property(c => c.CallName).HasMaxLength(ColumnSizes.CallName);
        });
    }
}
