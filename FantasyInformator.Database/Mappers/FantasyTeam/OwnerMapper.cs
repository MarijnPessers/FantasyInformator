using FantasyInformator.Database.Mappers.Tracking;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Mappers.FantasyTeam;

public static class OwnerMapper
{
    public static AppFantasyTeam.Owner? ToModel(DatabaseFantasyTeam.Owner? owner) =>
        ToModel(owner, new MappingContext());

    public static AppFantasyTeam.Owner? ToModel(DatabaseFantasyTeam.Owner? owner, MappingContext context)
    {
        if (owner is null)
        {
            return null;
        }

        if (context.TryGetMapped(owner, out AppFantasyTeam.Owner? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(owner, new AppFantasyTeam.Owner
        {
            ExternalId = owner.ExternalId,
            Name = owner.Name,
        });
        context.Register(owner, mapped);

        mapped.FantasyTeams.AddRange(context.MapList(owner.FantasyTeams, FantasyTeamMapper.ToModel));

        return mapped;
    }
}
