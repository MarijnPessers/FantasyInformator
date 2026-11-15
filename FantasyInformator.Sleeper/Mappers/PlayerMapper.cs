using FantasyInformator.Domain.Nfl;
using FantasyInformator.Domain.Player;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Mappers;

/// <summary>
/// Maps the Sleeper players and stat lines to the Player group.
/// Links to other records are stubs that hold only the key (<c>ExternalId</c>, or <c>Abbreviation</c> for a position).
/// </summary>
public static class PlayerMapper
{
    /// <summary>Throws when the player id or the team code is empty.</summary>
    public static Player ToPlayer(SleeperModels.Player player)
    {
        ArgumentException.ThrowIfNullOrEmpty(player.PlayerId);

        if (player.Team is not null)
        {
            ArgumentException.ThrowIfNullOrEmpty(player.Team);
        }

        return new()
        {
            ExternalId = player.PlayerId,
            Name = $"{player.FirstName} {player.LastName}".Trim(),
            CurrentNflTeam = player.Team is null
            ? null
            : new NflTeam { ExternalId = player.Team },
            InjuryStatus = InjuryStatuses.ToInjuryStatus(player.InjuryStatus),
        };
    }

    /// <summary>
    /// One <c>PlayerPosition</c> per different position among <c>position</c> and <c>fantasy_positions</c>.
    /// The caller adds them to the player. Throws when a position code is empty.
    /// </summary>
    public static List<PlayerPosition> ToPlayerPositions(SleeperModels.Player player)
    {
        var positionCodes = player.FantasyPositions.AsEnumerable();

        if (player.Position is not null)
        {
            positionCodes = positionCodes.Prepend(player.Position);
        }

        List<string> distinctPositionCodes = [.. positionCodes.Distinct()];

        foreach (var positionCode in distinctPositionCodes)
        {
            ArgumentException.ThrowIfNullOrEmpty(positionCode);
        }

        return [.. distinctPositionCodes
            .Select(x => new PlayerPosition
            {
                Position = new Position { Abbreviation = x },
            })];
    }

    /// <summary>
    /// <c>IsHome</c> is true when the team of the stat line is the home team of the game. The stat line
    /// holds the team of that game, so a traded player is handled. Throws when the player id is empty.
    /// </summary>
    public static GameAppearance ToGameAppearance(
        SleeperModels.StatLine statLine,
        SleeperModels.Game game)
    {
        ArgumentException.ThrowIfNullOrEmpty(statLine.PlayerId);

        return new()
        {
            Player = new Player { ExternalId = statLine.PlayerId },
            Game = new Game { ExternalId = statLine.GameId },
            IsHome = statLine.Team == game.Home,
        };
    }
}
