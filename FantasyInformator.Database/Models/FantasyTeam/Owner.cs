
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.FantasyTeam;

public class Owner : TrackedModel
{
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    public List<FantasyTeam> FantasyTeams { get; init; } = [];
}
