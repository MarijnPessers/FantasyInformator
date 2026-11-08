using FantasyInformator.Database.Mappers.Tracking;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Mappers.FantasyTeam;

public static class OwnerMapper
{
    public static AppFantasyTeam.Owner Map(this DatabaseFantasyTeam.Owner owner) =>
        TrackedModelMapper.MapToAppModel(owner, new AppFantasyTeam.Owner
        {
            ExternalId = owner.ExternalId,
            Name = owner.Name,
            FantasyTeams = [.. owner.FantasyTeams.Select(x => x.Map())],
        });

    public static DatabaseFantasyTeam.Owner Map(this AppFantasyTeam.Owner owner) =>
        TrackedModelMapper.MapToDbModel(owner, new DatabaseFantasyTeam.Owner
        {
            ExternalId = owner.ExternalId,
            Name = owner.Name,
            FantasyTeams = [.. owner.FantasyTeams.Select(x => x.Map())],
        });
}
