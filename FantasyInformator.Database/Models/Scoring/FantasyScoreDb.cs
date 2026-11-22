
using FantasyInformator.Database.Models.Leagues;
using FantasyInformator.Database.Models.Players;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.Scoring;

/// <summary>The fantasy points a player scored in a game for a league instance.</summary>
public class FantasyScoreDb : TrackedModelDb
{
    public Guid GameAppearanceId { get; set; }

    public GameAppearanceDb? GameAppearance { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstanceDb? LeagueInstance { get; set; }

    /// <summary>Null when the player did not play. 0 when the player was active but scored nothing.</summary>
    public decimal? Points { get; set; }
}
