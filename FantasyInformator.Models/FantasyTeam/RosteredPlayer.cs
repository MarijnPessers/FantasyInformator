
using FantasyInformator.Models.Lineup;
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.FantasyTeam;

/// <summary>A player on the roster of a fantasy team. The league comes through the team.</summary>
public class RosteredPlayer : TrackedModel
{
    public Guid FantasyTeamId { get; set; }

    public FantasyTeam? FantasyTeam { get; set; }

    public Guid PlayerId { get; set; }

    public Player.Player? Player { get; set; }

    /// <summary>The slot the player is in on this team right now, such as QB or Bench.</summary>
    public Guid SlotId { get; set; }

    public Slot? Slot { get; set; }
}
