
using FantasyInformator.Models.Tracking;

namespace FantasyInformator.Models.FantasyTeam;

public class Owner : TrackedModel
{
    public required string ExternalId { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<FantasyTeam> FantasyTeams { get; init; } = [];
}
