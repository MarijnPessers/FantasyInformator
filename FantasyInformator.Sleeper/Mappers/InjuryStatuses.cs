using FantasyInformator.Models.Enums;

namespace FantasyInformator.Sleeper.Mappers;

/// <summary>The Sleeper injury statuses the application keeps, and how they map to <see cref="InjuryStatus"/>.</summary>
public static class InjuryStatuses
{
    public const string InjuredReserve = "IR";

    public const string PhysicallyUnableToPerform = "PUP";

    public const string Out = "Out";

    public const string Questionable = "Questionable";

    public const string Doubtful = "Doubtful";

    private static readonly Dictionary<string, InjuryStatus> StatusBySleeperText = new(StringComparer.OrdinalIgnoreCase)
    {
        [InjuredReserve] = InjuryStatus.IR,
        [PhysicallyUnableToPerform] = InjuryStatus.PUP,
        [Out] = InjuryStatus.OUT,
        [Questionable] = InjuryStatus.Q,
        [Doubtful] = InjuryStatus.D,
    };

    /// <summary>A missing or unknown Sleeper text gives <see cref="InjuryStatus.None"/>.</summary>
    public static InjuryStatus ToInjuryStatus(string? sleeperText) =>
        sleeperText is null
            ? InjuryStatus.None
            : StatusBySleeperText.GetValueOrDefault(sleeperText, InjuryStatus.None);
}
