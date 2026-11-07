using FantasyInformator.Database.Mappers.League;
using FantasyInformator.Database.Mappers.Tracking;
using AppFantasyTeam = FantasyInformator.Models.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Mappers.FantasyTeam;

public static class FantasyTeamMapper
{
    public static AppFantasyTeam.FantasyTeam? ToModel(DatabaseFantasyTeam.FantasyTeam? fantasyTeam) =>
        ToModel(fantasyTeam, new MappingContext());

    public static AppFantasyTeam.FantasyTeam? ToModel(
        DatabaseFantasyTeam.FantasyTeam? fantasyTeam,
        MappingContext context)
    {
        if (fantasyTeam is null)
        {
            return null;
        }

        if (context.TryGetMapped(fantasyTeam, out AppFantasyTeam.FantasyTeam? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(fantasyTeam, new AppFantasyTeam.FantasyTeam
        {
            ExternalId = fantasyTeam.ExternalId,
            OwnerId = fantasyTeam.OwnerId,
            LeagueInstanceId = fantasyTeam.LeagueInstanceId,
            Name = fantasyTeam.Name,
        });
        context.Register(fantasyTeam, mapped);

        mapped.Owner = OwnerMapper.ToModel(fantasyTeam.Owner, context);
        mapped.LeagueInstance = LeagueInstanceMapper.ToModel(fantasyTeam.LeagueInstance, context);
        mapped.RosteredPlayers.AddRange(context.MapList(fantasyTeam.RosteredPlayers, RosteredPlayerMapper.ToModel));

        return mapped;
    }
}
