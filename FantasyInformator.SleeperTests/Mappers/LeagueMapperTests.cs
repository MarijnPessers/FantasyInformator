using AutoFixture;
using FantasyInformator.Sleeper.Mappers;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.SleeperTests.Mappers;

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
    public void ToLeague_MapsName()
    {
        var league = _fixture.Create<SleeperModels.League>();

        var result = LeagueMapper.ToLeague(league);

        Assert.AreEqual(league.Name, result.Name);
    }

    [TestMethod]
    public void ToLeagueInstance_MapsExternalIdAndSeasonYear()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.Season = "2025";

        var result = LeagueMapper.ToLeagueInstance(league);

        Assert.AreEqual(league.LeagueId, result.ExternalId);
        Assert.AreEqual(2025, result.Season!.Year);
    }

    [TestMethod]
    public void ToLeagueInstance_SeasonNotANumber_Throws()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.Season = _fixture.Create<string>();

        Assert.ThrowsExactly<FormatException>(() => LeagueMapper.ToLeagueInstance(league));
    }

    [TestMethod]
    public void ToLeagueSlots_CountsEachRosterPosition()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.RosterPositions = [QuarterbackCode, QuarterbackCode, FlexCode, BenchCode, BenchCode, BenchCode];
        league.Settings = new SleeperModels.LeagueSettings { ReserveSlots = 0, TaxiSlots = 0 };

        var result = LeagueMapper.ToLeagueSlots(league);

        Assert.HasCount(3, result);
        Assert.AreEqual(2, CountOf(result, QuarterbackCode));
        Assert.AreEqual(1, CountOf(result, FlexCode));
        Assert.AreEqual(3, CountOf(result, BenchCode));
    }

    [TestMethod]
    public void ToLeagueSlots_ReserveAndTaxiSlots_AreAdded()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.RosterPositions = [QuarterbackCode];
        league.Settings = new SleeperModels.LeagueSettings { ReserveSlots = 2, TaxiSlots = 3 };

        var result = LeagueMapper.ToLeagueSlots(league);

        Assert.HasCount(3, result);
        Assert.AreEqual(2, CountOf(result, InjuredReserveCode));
        Assert.AreEqual(3, CountOf(result, TaxiCode));
    }

    [TestMethod]
    public void ToLeagueSlots_NoReserveAndNoTaxi_AreNotReturned()
    {
        var league = _fixture.Create<SleeperModels.League>();
        league.RosterPositions = [QuarterbackCode];
        league.Settings = new SleeperModels.LeagueSettings { ReserveSlots = 0, TaxiSlots = 0 };

        var result = LeagueMapper.ToLeagueSlots(league);

        Assert.HasCount(1, result);
    }

    private static int CountOf(List<FantasyInformator.Models.LeagueSlot> leagueSlots, string slotCode) =>
        leagueSlots.Single(leagueSlot => leagueSlot.Slot!.ExternalId == slotCode).Count;
}
