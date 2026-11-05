using AppModels = FantasyInformator.Models;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.Database.Mappers;

public static class LeagueInstanceMapper
{
    public static AppModels.LeagueInstance? ToModel(DatabaseModels.LeagueInstance? leagueInstance) =>
        ToModel(leagueInstance, new MappingContext());

    public static AppModels.LeagueInstance? ToModel(DatabaseModels.LeagueInstance? leagueInstance, MappingContext context)
    {
        if (leagueInstance is null)
        {
            return null;
        }

        if (context.TryGetMapped(leagueInstance, out AppModels.LeagueInstance? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(leagueInstance, new AppModels.LeagueInstance
        {
            ExternalId = leagueInstance.ExternalId,
            LeagueId = leagueInstance.LeagueId,
            SeasonId = leagueInstance.SeasonId,
        });
        context.Register(leagueInstance, mapped);

        mapped.League = LeagueMapper.ToModel(leagueInstance.League, context);
        mapped.Season = SeasonMapper.ToModel(leagueInstance.Season, context);
        mapped.LeagueSlots = context.MapList(leagueInstance.LeagueSlots, LeagueSlotMapper.ToModel);
        mapped.FantasyTeams = context.MapList(leagueInstance.FantasyTeams, FantasyTeamMapper.ToModel);
        mapped.FantasyScores = context.MapList(leagueInstance.FantasyScores, FantasyScoreMapper.ToModel);
        mapped.PlayerSeasonStats = context.MapList(leagueInstance.PlayerSeasonStats, PlayerSeasonStatsMapper.ToModel);

        return mapped;
    }
}
