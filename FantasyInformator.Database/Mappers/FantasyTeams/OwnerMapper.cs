using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.FantasyTeams;
using AppFantasyTeam = FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Mappers.FantasyTeams;

public static class OwnerMapper
{
    public static AppFantasyTeam.Owner Map(this OwnerDb owner) =>
        TrackedModelMapper.MapToAppModel(owner, new AppFantasyTeam.Owner
        {
            ExternalId = owner.ExternalId,
            Name = owner.Name,
            FantasyTeams = [.. owner.FantasyTeams.Select(x => x.Map())],
        });

    public static OwnerDb Map(this AppFantasyTeam.Owner owner) =>
        TrackedModelMapper.Map(owner, new OwnerDb
        {
            ExternalId = owner.ExternalId,
            Name = owner.Name,
            FantasyTeams = [.. owner.FantasyTeams.Select(x => x.Map())],
        });
}
