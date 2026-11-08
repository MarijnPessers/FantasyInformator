using System.ComponentModel;

namespace FantasyInformator.Models.Enums;

/// <summary>
/// The injury status of a player. The values are the abbreviations, and each has its full name as a description.
/// IR, PUP and OUT let a player move to an IR slot. Q and D are shown only and never keep a player out of the lineup.
/// </summary>
public enum InjuryStatus
{
    [Description("No injury status")]
    None,

    [Description("Questionable")]
    Q,

    [Description("Doubtful")]
    D,

    [Description("Out")]
    OUT,

    [Description("Injured reserve")]
    IR,

    [Description("Physically unable to perform")]
    PUP,
}
