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
    public static AppLeague.LeagueInstance? Map(this DatabaseLeague.LeagueInstance? leagueInstance) =>
        leagueInstance.Map(new MappingContext());

    public static AppLeague.LeagueInstance? Map(
        this DatabaseLeague.LeagueInstance? leagueInstance,
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

        mapped.League = leagueInstance.League.Map(context);
        mapped.Season = leagueInstance.Season.Map(context);
        mapped.LeagueSlots.AddRange(context.MapList(leagueInstance.LeagueSlots, LeagueSlotMapper.Map));
        mapped.FantasyTeams.AddRange(context.MapList(leagueInstance.FantasyTeams, FantasyTeamMapper.Map));
        mapped.FantasyScores.AddRange(
            context.MapList(leagueInstance.FantasyScores, FantasyScoreMapper.Map));
        mapped.PlayerSeasonStats.AddRange(context.MapList(
            leagueInstance.PlayerSeasonStats,
            PlayerSeasonStatsMapper.Map));

        return mapped;
    }
}
