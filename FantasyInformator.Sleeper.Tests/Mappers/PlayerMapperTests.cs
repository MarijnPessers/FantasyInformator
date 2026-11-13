using AutoFixture;
using FantasyInformator.Models.Enums;
using FantasyInformator.Sleeper.Mappers;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Tests.Mappers;

[TestClass]
public class PlayerMapperTests
{
    private const string RunningBackCode = "RB";
    private const string WideReceiverCode = "WR";
    private const string UnknownStatus = "SomeOtherStatus";

    private readonly Fixture _fixture = new();

    [TestMethod]
    public void ToPlayer_ShouldMapExternalIdNameAndTeam_WhenPlayerIsGiven()
    {
        var player = _fixture.Create<SleeperModels.Player>();

        var result = PlayerMapper.ToPlayer(player);

        Assert.AreEqual(player.PlayerId, result.ExternalId);
        Assert.AreEqual($"{player.FirstName} {player.LastName}", result.Name);
        Assert.AreEqual(player.Team, result.CurrentNflTeam!.ExternalId);
    }

    [TestMethod]
    public void ToPlayer_ShouldLeaveNflTeamNull_WhenPlayerHasNoTeam()
    {
        var player = _fixture.Create<SleeperModels.Player>();
        player.Team = null;

        var result = PlayerMapper.ToPlayer(player);

        Assert.IsNull(result.CurrentNflTeam);
    }

    [TestMethod]
    public void ToPlayer_ShouldThrow_WhenPlayerIdIsEmpty()
    {
        var player = _fixture.Create<SleeperModels.Player>();
        player.PlayerId = string.Empty;

        _ = Assert.ThrowsExactly<ArgumentException>(() => PlayerMapper.ToPlayer(player));
    }

    [TestMethod]
    public void ToPlayer_ShouldThrow_WhenTeamIsEmpty()
    {
        var player = _fixture.Create<SleeperModels.Player>();
        player.Team = string.Empty;

        _ = Assert.ThrowsExactly<ArgumentException>(() => PlayerMapper.ToPlayer(player));
    }

    [TestMethod]
    [DataRow(InjuryStatuses.InjuredReserve, InjuryStatus.IR, true)]
    [DataRow(InjuryStatuses.PhysicallyUnableToPerform, InjuryStatus.PUP, true)]
    [DataRow(InjuryStatuses.Out, InjuryStatus.OUT, true)]
    [DataRow(InjuryStatuses.Questionable, InjuryStatus.Q, false)]
    [DataRow(InjuryStatuses.Doubtful, InjuryStatus.D, false)]
    [DataRow(UnknownStatus, InjuryStatus.None, false)]
    [DataRow(null, InjuryStatus.None, false)]
    public void ToPlayer_ShouldMapStatusAndIrEligibility_WhenInjuryStatusIsGiven(
        string? injuryStatus,
        InjuryStatus expectedStatus,
        bool expectedIrEligible)
    {
        var player = _fixture.Create<SleeperModels.Player>();
        player.InjuryStatus = injuryStatus;

        var result = PlayerMapper.ToPlayer(player);

        Assert.AreEqual(expectedStatus, result.InjuryStatus);
        Assert.AreEqual(expectedIrEligible, result.IsIrEligible);
    }

    [TestMethod]
    public void ToPlayerPositions_ShouldReturnEachOnce_WhenPositionIsAlsoInFantasyPositions()
    {
        var player = new SleeperModels.Player
        {
            Position = RunningBackCode,
            FantasyPositions = [RunningBackCode, WideReceiverCode],
        };

        var result = PlayerMapper.ToPlayerPositions(player);

        Assert.AreSequenceEqual(
            [RunningBackCode, WideReceiverCode], result.Select(x => x.Position!.Abbreviation).ToList(), SequenceOrder.InAnyOrder);
    }

    [TestMethod]
    public void ToPlayerPositions_ShouldUseFantasyPositions_WhenThereIsNoPosition()
    {
        var player = new SleeperModels.Player
        {
            Position = null,
            FantasyPositions = [WideReceiverCode],
        };

        var result = PlayerMapper.ToPlayerPositions(player);

        Assert.HasCount(1, result);
        Assert.AreEqual(WideReceiverCode, result[0].Position!.Abbreviation);
    }

    [TestMethod]
    public void ToPlayerPositions_ShouldThrow_WhenPositionCodeIsEmpty()
    {
        var player = new SleeperModels.Player
        {
            Position = null,
            FantasyPositions = [string.Empty],
        };

        _ = Assert.ThrowsExactly<ArgumentException>(() => PlayerMapper.ToPlayerPositions(player));
    }

    [TestMethod]
    public void ToGameAppearance_ShouldSetIsHomeToTrue_WhenTeamIsHomeTeam()
    {
        var game = CreateGame();
        var statLine = _fixture.Create<SleeperModels.StatLine>();
        statLine.Team = game.Home;

        var result = PlayerMapper.ToGameAppearance(statLine, game);

        Assert.IsTrue(result.IsHome);
        Assert.AreEqual(statLine.PlayerId, result.Player!.ExternalId);
        Assert.AreEqual(statLine.GameId, result.Game!.ExternalId);
    }

    [TestMethod]
    public void ToGameAppearance_ShouldSetIsHomeToFalse_WhenTeamIsAwayTeam()
    {
        var game = CreateGame();
        var statLine = _fixture.Create<SleeperModels.StatLine>();
        statLine.Team = game.Away;

        var result = PlayerMapper.ToGameAppearance(statLine, game);

        Assert.IsFalse(result.IsHome);
    }

    [TestMethod]
    public void ToGameAppearance_ShouldThrow_WhenPlayerIdIsEmpty()
    {
        var game = CreateGame();
        var statLine = _fixture.Create<SleeperModels.StatLine>();
        statLine.PlayerId = string.Empty;

        _ = Assert.ThrowsExactly<ArgumentException>(() => PlayerMapper.ToGameAppearance(statLine, game));
    }

    private SleeperModels.Game CreateGame() =>
        new()
        {
            GameId = _fixture.Create<string>(),
            Home = _fixture.Create<string>(),
            Away = _fixture.Create<string>(),
        };
}
