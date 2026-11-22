using FantasyInformator.Database.Models.Administration;
using AppAdministration = FantasyInformator.Domain.Administration;

namespace FantasyInformator.Database.Mappers.Administration;

public static class LastSleeperCallMapper
{
    /// <summary>It has no tracking fields and no links, so it needs no context.</summary>
    public static AppAdministration.LastSleeperCall? Map(
        this LastSleeperCallDb? lastSleeperCall) =>
        lastSleeperCall is null
            ? null
            : new AppAdministration.LastSleeperCall
            {
                CallName = lastSleeperCall.CallName,
                CalledAtUtc = lastSleeperCall.CalledAtUtc,
            };
}
