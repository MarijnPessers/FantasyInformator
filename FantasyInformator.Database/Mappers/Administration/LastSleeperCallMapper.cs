using AppAdministration = FantasyInformator.Models.Administration;
using DatabaseAdministration = FantasyInformator.Database.Models.Administration;

namespace FantasyInformator.Database.Mappers.Administration;

public static class LastSleeperCallMapper
{
    /// <summary>It has no tracking fields and no links, so it needs no context.</summary>
    public static AppAdministration.LastSleeperCall? ToModel(
        DatabaseAdministration.LastSleeperCall? lastSleeperCall) =>
        lastSleeperCall is null
            ? null
            : new AppAdministration.LastSleeperCall
            {
                CallName = lastSleeperCall.CallName,
                CalledAtUtc = lastSleeperCall.CalledAtUtc,
            };
}
