using AutoFixture;
using FantasyInformator.Domain.Lineups;
using FantasyInformator.Sleeper.Mappers;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Tests.Mappers;

[TestClass]
public class LeagueMapperTests
{
    private const string QuarterbackCode = "QB";
    private const string FlexCode = "FLEX";
    private const string BenchCode = "BN";
    private const string InjuredReserveCode = "IR";
    private const string TaxiCode = "TAX";

    private readonly Fixture _fixture = new();

    [TestMethod]
    public void ToLeague_ShouldMapName_WhenLeagueIsGiven()
    {
        var league = _fixture.Create<SleeperModels.League>();

        var result = LeagueMapper.ToLeague(league);

        Assert.AreEqual(league.Name, result.Name);
    }

    [TestMethod]
    public void ToLeague_ShouldThrow_WhenNameIsEmpty()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.Name = string.Empty;

        _ = Assert.ThrowsExactly<ArgumentException>(() => LeagueMapper.ToLeague(league));
    }

    [TestMethod]
    public void ToLeagueInstance_ShouldMapExternalIdAndSeasonYear_WhenLeagueIsGiven()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.Season = "2025";

        var result = LeagueMapper.ToLeagueInstance(league);

        Assert.AreEqual(league.LeagueId, result.ExternalId);
        Assert.AreEqual(2025, result.Season!.Year);
    }

    [TestMethod]
    public void ToLeagueInstance_ShouldThrow_WhenSeasonIsNotANumber()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.Season = _fixture.Create<string>();

        _ = Assert.ThrowsExactly<FormatException>(() => LeagueMapper.ToLeagueInstance(league));
    }

    [TestMethod]
    public void ToLeagueInstance_ShouldThrow_WhenLeagueIdIsEmpty()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.LeagueId = string.Empty;
        league.Season = "2025";

        _ = Assert.ThrowsExactly<ArgumentException>(() => LeagueMapper.ToLeagueInstance(league));
    }

    [TestMethod]
    public void ToLeagueSlots_ShouldCountEachRosterPosition_WhenLeagueIsGiven()
    {
        var league = new SleeperModels.League
        {
            RosterPositions = [QuarterbackCode, QuarterbackCode, FlexCode, BenchCode, BenchCode, BenchCode],
            Settings = new SleeperModels.LeagueSettings { ReserveSlots = 0, TaxiSlots = 0 },
        };

        var result = LeagueMapper.ToLeagueSlots(league);

        Assert.HasCount(3, result);
        Assert.AreEqual(2, CountOf(result, QuarterbackCode));
        Assert.AreEqual(1, CountOf(result, FlexCode));
        Assert.AreEqual(3, CountOf(result, BenchCode));
    }

    [TestMethod]
    public void ToLeagueSlots_ShouldAddReserveAndTaxiSlots_WhenLeagueHasThem()
    {
        var league = new SleeperModels.League
        {
            RosterPositions = [QuarterbackCode],
            Settings = new SleeperModels.LeagueSettings { ReserveSlots = 2, TaxiSlots = 3 },
        };

        var result = LeagueMapper.ToLeagueSlots(league);

        Assert.HasCount(3, result);
        Assert.AreEqual(2, CountOf(result, InjuredReserveCode));
        Assert.AreEqual(3, CountOf(result, TaxiCode));
    }

    [TestMethod]
    public void ToLeagueSlots_ShouldNotReturnReserveAndTaxiSlots_WhenLeagueHasNone()
    {
        var league = new SleeperModels.League
        {
            RosterPositions = [QuarterbackCode],
            Settings = new SleeperModels.LeagueSettings { ReserveSlots = 0, TaxiSlots = 0 },
        };

        var result = LeagueMapper.ToLeagueSlots(league);

        Assert.HasCount(1, result);
    }

    [TestMethod]
    public void ToLeagueSlots_ShouldThrow_WhenSlotCodeIsEmpty()
    {
        var league = new SleeperModels.League { RosterPositions = [string.Empty] };

        _ = Assert.ThrowsExactly<ArgumentException>(() => LeagueMapper.ToLeagueSlots(league));
    }

    private static int CountOf(List<LeagueSlot> leagueSlots, string slotCode) =>
        leagueSlots.Single(x => x.Slot!.ExternalId == slotCode).Count;
}
