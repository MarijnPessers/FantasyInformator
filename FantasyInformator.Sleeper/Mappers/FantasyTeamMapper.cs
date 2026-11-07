using System.Globalization;
using FantasyInformator.Models.FantasyTeam;
using FantasyInformator.Models.Lineup;
using FantasyInformator.Models.Player;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Mappers;

/// <summary>
/// Maps the Sleeper users and rosters to the FantasyTeam group.
/// Links to other records are stubs that hold only the <c>ExternalId</c>.
/// </summary>
public static class FantasyTeamMapper
{
    public static Owner ToOwner(SleeperModels.User user) =>
        new()
        {
            ExternalId = user.UserId,
            Name = user.DisplayName,
        };

    public static Owner ToOwner(SleeperModels.LeagueUser leagueUser) =>
        new()
        {
            ExternalId = leagueUser.UserId,
            Name = leagueUser.DisplayName,
        };

    /// <summary>
    /// The caller finds the league user whose id equals <c>owner_id</c> of the roster. The team name is empty
    /// when there is no such user or the user never set a team name.
    /// </summary>
    public static FantasyTeam ToFantasyTeam(
        SleeperModels.Roster roster,
        SleeperModels.LeagueUser? leagueUser) =>
        new()
        {
            ExternalId = roster.RosterId.ToString(CultureInfo.InvariantCulture),
            Owner = roster.OwnerId is null
                ? null
                : new Owner { ExternalId = roster.OwnerId },
            Name = leagueUser?.Metadata.TeamName ?? string.Empty,
        };

    /// <summary>
    /// Gives every player on the roster the slot he is in. A starter takes the slot at his place in the
    /// league's <c>roster_positions</c> without the bench; that <c>starters</c> follows that order is not verified.
    /// A player on <c>reserve</c> is in IR, one on <c>taxi</c> in TAX, and every other player is on the bench.
    /// Throws when a starter's place is beyond the starting slots of the league.
    /// </summary>
    public static List<RosteredPlayer> ToRosteredPlayers(
        SleeperModels.Roster roster,
        IReadOnlyList<string> rosterPositions)
    {
        List<string> startingSlots = [.. rosterPositions.Where(x => x != SlotCodes.Bench)];

        return [.. roster.Players
            .Select(x => new RosteredPlayer
            {
                Player = new Player { ExternalId = x },
                Slot = new Slot { ExternalId = FindSlotCode(roster, x, startingSlots) },
            })];
    }

    private static string FindSlotCode(SleeperModels.Roster roster, string playerId, List<string> startingSlots)
    {
        var starterIndex = roster.Starters.IndexOf(playerId);

        return starterIndex switch
        {
            >= 0 => startingSlots[starterIndex],
            _ when roster.Reserve.Contains(playerId) => SlotCodes.InjuredReserve,
            _ when roster.Taxi.Contains(playerId) => SlotCodes.Taxi,
            _ => SlotCodes.Bench,
        };
    }
}
