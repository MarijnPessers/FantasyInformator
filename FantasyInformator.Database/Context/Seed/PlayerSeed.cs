using FantasyInformator.Database.Models.Player;

namespace FantasyInformator.Database.Context.Seed;

/// <summary>The abbreviations of the seeded positions.</summary>
public static class PositionCodes
{
    public const string Quarterback = "QB";

    public const string RunningBack = "RB";

    public const string WideReceiver = "WR";

    public const string TightEnd = "TE";

    public const string Kicker = "K";

    public const string DefensiveLine = "DL";

    public const string DefensiveEnd = "DE";

    public const string Linebacker = "LB";

    public const string DefensiveBack = "DB";

    public const string Cornerback = "CB";

    public const string Safety = "S";
}

/// <summary>
/// The positions a player can play. A position that is not here is added when a player arrives with it.
/// </summary>
public static class PlayerSeed
{
    public static readonly IReadOnlyList<Position> Positions =
    [
        CreatePosition("Quarterback", PositionCodes.Quarterback, false),
        CreatePosition("Running back", PositionCodes.RunningBack, false),
        CreatePosition("Wide receiver", PositionCodes.WideReceiver, false),
        CreatePosition("Tight end", PositionCodes.TightEnd, false),
        CreatePosition("Kicker", PositionCodes.Kicker, false),
        CreatePosition("Defensive line", PositionCodes.DefensiveLine, true),
        CreatePosition("Defensive end", PositionCodes.DefensiveEnd, true),
        CreatePosition("Linebacker", PositionCodes.Linebacker, false),
        CreatePosition("Defensive back", PositionCodes.DefensiveBack, false),
        CreatePosition("Cornerback", PositionCodes.Cornerback, false),
        CreatePosition("Safety", PositionCodes.Safety, false),
    ];

    public static Guid PositionId(string code) =>
        SeedIds.For(nameof(Position), code);

    private static Position CreatePosition(string name, string code, bool isDominant) =>
        new()
        {
            Id = PositionId(code),
            Name = name,
            Abbreviation = code,
            IsDominant = isDominant,
            CreatedUtc = SeedIds.SeedDateUtc,
            UpdatedUtc = SeedIds.SeedDateUtc,
        };
}
