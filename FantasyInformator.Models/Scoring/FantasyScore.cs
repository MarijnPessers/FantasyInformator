
using FantasyInformator.Models.League;
using FantasyInformator.Models.Player;
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.Scoring;

/// <summary>The fantasy points a player scored in a game for a league instance.</summary>
public class FantasyScore : TrackedModel
{
    public Guid GameAppearanceId { get; set; }

    public GameAppearance? GameAppearance { get; set; }

    public Guid LeagueInstanceId { get; set; }

    public LeagueInstance? LeagueInstance { get; set; }

    /// <summary>Null when the player did not play. 0 when the player was active but scored nothing.</summary>
    public decimal? Points { get; set; }
}
