using FantasyInformator.Models.Nfl;
using FantasyInformator.Models.Player;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Mappers;

/// <summary>
/// Maps the Sleeper players and stat lines to the Player group.
/// Links to other records are stubs that hold only the key (<c>ExternalId</c>, or <c>Abbreviation</c> for a position).
/// </summary>
public static class PlayerMapper
{
    public static Player ToPlayer(SleeperModels.Player player) =>
        new()
        {
            ExternalId = player.PlayerId,
            Name = $"{player.FirstName} {player.LastName}".Trim(),
            CurrentNflTeam = player.Team is null
            ? null
            : new NflTeam { ExternalId = player.Team },
        };

    /// <summary>
    /// One <c>PlayerPosition</c> per different position among <c>position</c> and <c>fantasy_positions</c>.
    /// The caller adds them to the player.
    /// </summary>
    public static List<PlayerPosition> ToPlayerPositions(SleeperModels.Player player)
    {
        var positionCodes = player.FantasyPositions.AsEnumerable();

        if (player.Position is not null)
        {
            positionCodes = positionCodes.Prepend(player.Position);
        }

        return [.. positionCodes
            .Distinct()
            .Select(x => new PlayerPosition
            {
                Position = new Position { Abbreviation = x },
            })];
    }

    /// <summary>
    /// <c>IsHome</c> is true when the team of the stat line is the home team of the game. The stat line
    /// holds the team of that game, so a traded player is handled.
    /// </summary>
    public static GameAppearance ToGameAppearance(
        SleeperModels.StatLine statLine,
        SleeperModels.Game game) =>
        new()
        {
            Player = new Player { ExternalId = statLine.PlayerId },
            Game = new Game { ExternalId = statLine.GameId },
            IsHome = statLine.Team == game.Home,
        };
}
