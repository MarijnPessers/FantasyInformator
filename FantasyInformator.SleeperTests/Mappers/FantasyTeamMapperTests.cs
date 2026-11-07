using AutoFixture;
using FantasyInformator.Sleeper.Mappers;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.SleeperTests.Mappers;

[TestClass]
public class FantasyTeamMapperTests
{
    private const string QuarterbackCode = "QB";
    private const string FlexCode = "FLEX";
    private const string BenchCode = "BN";
    private const string InjuredReserveCode = "IR";
    private const string TaxiCode = "TAX";

    private readonly Fixture _fixture = new();

    [TestMethod]
    public void ToOwner_User_MapsExternalIdAndName()
    {
        var user = _fixture.Create<SleeperModels.User>();

        var result = FantasyTeamMapper.ToOwner(user);

        Assert.AreEqual(user.UserId, result.ExternalId);
        Assert.AreEqual(user.DisplayName, result.Name);
    }

    [TestMethod]
    public void ToOwner_LeagueUser_MapsExternalIdAndName()
    {
        var leagueUser = _fixture.Create<SleeperModels.LeagueUser>();

        var result = FantasyTeamMapper.ToOwner(leagueUser);

        Assert.AreEqual(leagueUser.UserId, result.ExternalId);
        Assert.AreEqual(leagueUser.DisplayName, result.Name);
    }

    [TestMethod]
    public void ToFantasyTeam_MapsExternalIdOwnerAndTeamName()
    {
        var roster = _fixture.Create<SleeperModels.Roster>();
        roster.RosterId = 7;
        var leagueUser = _fixture.Create<SleeperModels.LeagueUser>();

        var result = FantasyTeamMapper.ToFantasyTeam(roster, leagueUser);

        Assert.AreEqual("7", result.ExternalId);
        Assert.AreEqual(roster.OwnerId, result.Owner!.ExternalId);
        Assert.AreEqual(leagueUser.Metadata.TeamName, result.Name);
    }

    [TestMethod]
    public void ToFantasyTeam_NoOwnerAndNoLeagueUser_OwnerIsNullAndNameIsEmpty()
    {
        var roster = _fixture.Create<SleeperModels.Roster>();
        roster.OwnerId = null;

        var result = FantasyTeamMapper.ToFantasyTeam(roster, null);

        Assert.IsNull(result.Owner);
        Assert.AreEqual(string.Empty, result.Name);
    }

    [TestMethod]
    public void ToFantasyTeam_LeagueUserWithoutTeamName_NameIsEmpty()
    {
        var roster = _fixture.Create<SleeperModels.Roster>();
        var leagueUser = _fixture.Create<SleeperModels.LeagueUser>();
        leagueUser.Metadata.TeamName = null;

        var result = FantasyTeamMapper.ToFantasyTeam(roster, leagueUser);

        Assert.AreEqual(string.Empty, result.Name);
    }

    [TestMethod]
    public void ToRosteredPlayers_GivesEachPlayerHisSlot()
    {
        List<string> playerIds = [.. _fixture.CreateMany<string>(5)];
        var roster = new SleeperModels.Roster
        {
            Players = playerIds,
            Starters = [playerIds[0], playerIds[1]],
            Reserve = [playerIds[2]],
            Taxi = [playerIds[3]],
        };
        List<string> rosterPositions = [QuarterbackCode, FlexCode, BenchCode, BenchCode];

        var result = FantasyTeamMapper.ToRosteredPlayers(roster, rosterPositions);

        var slotCodeByPlayer = result.ToDictionary(
            r => r.Player!.ExternalId,
            r => r.Slot!.ExternalId);
        Assert.AreEqual(QuarterbackCode, slotCodeByPlayer[playerIds[0]]);
        Assert.AreEqual(FlexCode, slotCodeByPlayer[playerIds[1]]);
        Assert.AreEqual(InjuredReserveCode, slotCodeByPlayer[playerIds[2]]);
        Assert.AreEqual(TaxiCode, slotCodeByPlayer[playerIds[3]]);
        Assert.AreEqual(BenchCode, slotCodeByPlayer[playerIds[4]]);
    }

    [TestMethod]
    public void ToRosteredPlayers_MoreStartersThanStartingSlots_Throws()
    {
        List<string> playerIds = [.. _fixture.CreateMany<string>(2)];
        var roster = new SleeperModels.Roster { Players = playerIds, Starters = playerIds };
        List<string> rosterPositions = [QuarterbackCode, BenchCode];

        _ = Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => FantasyTeamMapper.ToRosteredPlayers(roster, rosterPositions));
    }
}
