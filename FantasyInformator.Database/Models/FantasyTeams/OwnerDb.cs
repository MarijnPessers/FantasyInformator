
using FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Models.FantasyTeams;

public class OwnerDb : TrackedModelDb
{
    public required string ExternalId { get; set; }

    public required string Name { get; set; }

    public List<FantasyTeamDb>? FantasyTeams { get; init; }
}
