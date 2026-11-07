using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Mappers.League;

public static class LeagueInstanceMapper
{
    public static AppLeague.LeagueInstance? ToModel(DatabaseLeague.LeagueInstance? leagueInstance) =>
        ToModel(leagueInstance, new MappingContext());

    public static AppLeague.LeagueInstance? ToModel(
        DatabaseLeague.LeagueInstance? leagueInstance,
        MappingContext context)
    {
        if (leagueInstance is null)
        {
            return null;
        }

        if (context.TryGetMapped(leagueInstance, out AppLeague.LeagueInstance? mapped))
        {
            return mapped;
        }

        mapped = TrackedModelMapper.Fill(leagueInstance, new AppLeague.LeagueInstance
        {
            ExternalId = leagueInstance.ExternalId,
            LeagueId = leagueInstance.LeagueId,
            SeasonId = leagueInstance.SeasonId,
        });
        context.Register(leagueInstance, mapped);

        mapped.League = LeagueMapper.ToModel(leagueInstance.League, context);
        mapped.Season = SeasonMapper.ToModel(leagueInstance.Season, context);
        mapped.LeagueSlots.AddRange(context.MapList(leagueInstance.LeagueSlots, LeagueSlotMapper.ToModel));
        mapped.FantasyTeams.AddRange(context.MapList(leagueInstance.FantasyTeams, FantasyTeamMapper.ToModel));
        mapped.FantasyScores.AddRange(context.MapList(leagueInstance.FantasyScores, FantasyScoreMapper.ToModel));
        mapped.PlayerSeasonStats.AddRange(context.MapList(
            leagueInstance.PlayerSeasonStats,
            PlayerSeasonStatsMapper.ToModel));

        return mapped;
    }
}
