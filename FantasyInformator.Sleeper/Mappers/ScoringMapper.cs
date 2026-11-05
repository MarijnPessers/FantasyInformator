using AppModels = FantasyInformator.Models;
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
    /// </summary>
    public static List<AppModels.FantasyScore> ToFantasyScores(SleeperModels.Matchup matchup)
    {
        return [.. matchup.PlayersPoints
            .Select(playerPoints => new AppModels.FantasyScore
            {
                GameAppearance = new AppModels.GameAppearance
                {
                    Player = new AppModels.Player { ExternalId = playerPoints.Key },
                },
                Points = (decimal?)playerPoints.Value,
            })];
    }
}
