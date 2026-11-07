using FantasyInformator.Database.Models.Administration;
using Microsoft.EntityFrameworkCore;

namespace FantasyInformator.Database.Configurations;

/// <summary>The tables of the Administration group.</summary>
public static class AdministrationConfiguration
{
    public static void Configure(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<LastSleeperCall>(lastSleeperCall =>
        {
            _ = lastSleeperCall.HasKey(x => x.CallName);
            _ = lastSleeperCall.Property(x => x.CallName).HasMaxLength(ColumnSizes.CallName);
        });
}
