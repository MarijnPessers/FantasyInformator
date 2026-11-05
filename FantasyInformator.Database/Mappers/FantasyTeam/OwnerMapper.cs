using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class OwnerMapper
{
    public static AppModels.Owner? ToModel(DatabaseModels.Owner? owner) => ToModel(owner, new MappingContext());

    public static AppModels.Owner? ToModel(DatabaseModels.Owner? owner, MappingContext context)
    {
        if (owner is null)
        {
            return null;
        }

        if (context.TryGetMapped(owner, out AppModels.Owner? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(owner, new AppModels.Owner
        {
            ExternalId = owner.ExternalId,
            Name = owner.Name,
        });
        context.Register(owner, mapped);

        mapped.FantasyTeams = context.MapList(owner.FantasyTeams, FantasyTeamMapper.ToModel);

        return mapped;
    }
}
