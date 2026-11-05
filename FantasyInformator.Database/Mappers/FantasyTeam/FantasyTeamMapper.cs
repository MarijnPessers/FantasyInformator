using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class FantasyTeamMapper
{
    public static AppModels.FantasyTeam? ToModel(DatabaseModels.FantasyTeam? fantasyTeam) =>
        ToModel(fantasyTeam, new MappingContext());

    public static AppModels.FantasyTeam? ToModel(DatabaseModels.FantasyTeam? fantasyTeam, MappingContext context)
    {
        if (fantasyTeam is null)
        {
            return null;
        }

        if (context.TryGetMapped(fantasyTeam, out AppModels.FantasyTeam? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(fantasyTeam, new AppModels.FantasyTeam
        {
            ExternalId = fantasyTeam.ExternalId,
            OwnerId = fantasyTeam.OwnerId,
            LeagueInstanceId = fantasyTeam.LeagueInstanceId,
            Name = fantasyTeam.Name,
        });
        context.Register(fantasyTeam, mapped);

        mapped.Owner = OwnerMapper.ToModel(fantasyTeam.Owner, context);
        mapped.LeagueInstance = LeagueInstanceMapper.ToModel(fantasyTeam.LeagueInstance, context);
        mapped.RosteredPlayers = context.MapList(fantasyTeam.RosteredPlayers, RosteredPlayerMapper.ToModel);

        return mapped;
    }
}
