namespace FantasyInformator.Nflverse.Mappers;

/// <summary>
/// The team codes where nflverse differs from Sleeper. Only the codes seen to differ are listed.
/// </summary>
public static class TeamCodes
{
    private static readonly Dictionary<string, string> SleeperCodeByNflverseCode = new()
    {
        ["LA"] = "LAR",
    };

    public static string ToSleeperCode(string nflverseCode) =>
        SleeperCodeByNflverseCode.GetValueOrDefault(nflverseCode, nflverseCode);
}
