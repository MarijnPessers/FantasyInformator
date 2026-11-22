
using FantasyInformator.Database.Models.Lineups;
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.FantasyTeams;

/// <summary>A player on the roster of a fantasy team. The league comes through the team.</summary>
public class RosteredPlayerDb : TrackedModelDb
{
    public Guid FantasyTeamId { get; set; }

    public FantasyTeamDb? FantasyTeam { get; set; }

    public Guid PlayerId { get; set; }

    public Players.PlayerDb? Player { get; set; }

    /// <summary>The slot the player is in on this team right now, such as QB or Bench.</summary>
    public Guid SlotId { get; set; }

    public SlotDb? Slot { get; set; }
}
