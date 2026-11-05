using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class LastSleeperCallMapper
{
    /// <summary>It has no tracking fields and no links, so it needs no context.</summary>
    public static AppModels.LastSleeperCall? ToModel(DatabaseModels.LastSleeperCall? lastSleeperCall)
    {
        if (lastSleeperCall is null)
        {
            return null;
        }

        return new AppModels.LastSleeperCall
        {
            CallName = lastSleeperCall.CallName,
            CalledAtUtc = lastSleeperCall.CalledAtUtc,
        };
    }
}
