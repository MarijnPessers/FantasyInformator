using FantasyInformator.Domain.Player;
using FantasyInformator.Domain.Scoring;
using SleeperModels = FantasyInformator.Sleeper.Services.Models;

namespace FantasyInformator.Sleeper.Mappers;

/// <summary>
/// Maps the Sleeper matchups to the Scoring group.
/// </summary>
public static class ScoringMapper
{
    /// <summary>
    /// One <c>FantasyScore</c> per player in the matchup. Null points stay null (the player did not play).
    /// The <c>GameAppearance</c> is a stub that holds only the player; the caller finds the game of that week.
    /// Throws when a player id is empty.
    /// </summary>
    public static List<FantasyScore> ToFantasyScores(SleeperModels.Matchup matchup)
    {
        foreach (var playerId in matchup.PlayersPoints.Keys)
        {
            ArgumentException.ThrowIfNullOrEmpty(playerId);
        }

        return [.. matchup.PlayersPoints
            .Select(x => new FantasyScore
            {
                GameAppearance = new GameAppearance
                {
                    Player = new Player { ExternalId = x.Key },
                },
                Points = (decimal?)x.Value,
            })];
    }
}
