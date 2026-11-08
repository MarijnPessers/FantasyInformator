using FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Context.Seed;

/// <summary>The Sleeper codes of the seeded slots.</summary>
public static class SlotCodes
{
    public const string Quarterback = "QB";

    public const string RunningBack = "RB";

    public const string WideReceiver = "WR";

    public const string TightEnd = "TE";

    public const string Flex = "FLEX";

    public const string SuperFlex = "SUPER_FLEX";

    public const string Kicker = "K";

    public const string DefensiveLine = "DL";

    public const string Linebacker = "LB";

    public const string IdpFlex = "IDP_FLEX";

    public const string DefensiveBack = "DB";

    public const string Bench = "BN";

    public const string InjuredReserve = "IR";

    public const string Taxi = "TAX";
}

/// <summary>
/// The slots of a lineup and the slots each position can start in. Every position fits BN, IR and TAX. Whether a
/// player may use IR depends on the injury flags of the player, not on the position.
/// </summary>
public static class LineupSeed
{
    public static readonly IReadOnlyList<Slot> Slots =
    [
        CreateSlot("Quarterback", SlotCodes.Quarterback, "QB"),
        CreateSlot("Running back", SlotCodes.RunningBack, "RB"),
        CreateSlot("Wide receiver", SlotCodes.WideReceiver, "WR"),
        CreateSlot("Tight end", SlotCodes.TightEnd, "TE"),
        CreateSlot("Flex", SlotCodes.Flex, "FLEX"),
        CreateSlot("Super flex", SlotCodes.SuperFlex, "SF"),
        CreateSlot("Kicker", SlotCodes.Kicker, "K"),
        CreateSlot("Defensive line", SlotCodes.DefensiveLine, "DL"),
        CreateSlot("Linebacker", SlotCodes.Linebacker, "LB"),
        CreateSlot("IDP flex", SlotCodes.IdpFlex, "IDP"),
        CreateSlot("Defensive back", SlotCodes.DefensiveBack, "DB"),
        CreateSlot("Bench", SlotCodes.Bench, "BN"),
        CreateSlot("Injured reserve", SlotCodes.InjuredReserve, "IR"),
        CreateSlot("Taxi squad", SlotCodes.Taxi, "TAX"),
    ];

    private static readonly string[] AnyPositionSlotCodes = [SlotCodes.Bench, SlotCodes.InjuredReserve, SlotCodes.Taxi];

    public static readonly IReadOnlyList<SlotEligibility> Eligibilities =
    [
        CreateEligibility(PositionCodes.Quarterback, SlotCodes.Quarterback, true),
        CreateEligibility(PositionCodes.Quarterback, SlotCodes.SuperFlex, true),

        CreateEligibility(PositionCodes.RunningBack, SlotCodes.RunningBack, true),
        CreateEligibility(PositionCodes.RunningBack, SlotCodes.Flex, false),
        CreateEligibility(PositionCodes.RunningBack, SlotCodes.SuperFlex, false),

        CreateEligibility(PositionCodes.WideReceiver, SlotCodes.WideReceiver, true),
        CreateEligibility(PositionCodes.WideReceiver, SlotCodes.Flex, false),
        CreateEligibility(PositionCodes.WideReceiver, SlotCodes.SuperFlex, false),

        CreateEligibility(PositionCodes.TightEnd, SlotCodes.TightEnd, true),
        CreateEligibility(PositionCodes.TightEnd, SlotCodes.Flex, false),
        CreateEligibility(PositionCodes.TightEnd, SlotCodes.SuperFlex, false),

        CreateEligibility(PositionCodes.Kicker, SlotCodes.Kicker, true),

        CreateEligibility(PositionCodes.DefensiveLine, SlotCodes.DefensiveLine, true),
        CreateEligibility(PositionCodes.DefensiveLine, SlotCodes.IdpFlex, false),

        CreateEligibility(PositionCodes.DefensiveEnd, SlotCodes.DefensiveLine, true),
        CreateEligibility(PositionCodes.DefensiveEnd, SlotCodes.IdpFlex, false),

        CreateEligibility(PositionCodes.Linebacker, SlotCodes.Linebacker, true),
        CreateEligibility(PositionCodes.Linebacker, SlotCodes.IdpFlex, true),

        CreateEligibility(PositionCodes.DefensiveBack, SlotCodes.DefensiveBack, true),
        CreateEligibility(PositionCodes.DefensiveBack, SlotCodes.IdpFlex, false),

        CreateEligibility(PositionCodes.Cornerback, SlotCodes.DefensiveBack, true),
        CreateEligibility(PositionCodes.Cornerback, SlotCodes.IdpFlex, false),

        CreateEligibility(PositionCodes.Safety, SlotCodes.DefensiveBack, true),
        CreateEligibility(PositionCodes.Safety, SlotCodes.IdpFlex, false),

        .. PlayerSeed.Positions
            .SelectMany(p => AnyPositionSlotCodes.Select(s => CreateEligibility(p.Abbreviation, s, false))),
    ];

    public static Guid SlotId(string code) =>
        SeedIds.For(nameof(Slot), code);

    private static Slot CreateSlot(string name, string code, string abbreviation) =>
        new()
        {
            Id = SlotId(code),
            ExternalId = code,
            Name = name,
            Abbreviation = abbreviation,
            CreatedUtc = SeedIds.SeedDateUtc,
            UpdatedUtc = SeedIds.SeedDateUtc,
        };

    private static SlotEligibility CreateEligibility(string positionCode, string slotCode, bool isPreferred) =>
        new()
        {
            Id = SeedIds.For(nameof(SlotEligibility), $"{positionCode}:{slotCode}"),
            PositionId = PlayerSeed.PositionId(positionCode),
            SlotId = SlotId(slotCode),
            IsPreferred = isPreferred,
            CreatedUtc = SeedIds.SeedDateUtc,
            UpdatedUtc = SeedIds.SeedDateUtc,
        };
}
