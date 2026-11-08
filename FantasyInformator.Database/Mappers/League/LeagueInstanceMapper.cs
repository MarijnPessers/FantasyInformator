using FantasyInformator.Database.Mappers.FantasyTeam;
using FantasyInformator.Database.Mappers.Lineup;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using AppLeague = FantasyInformator.Models.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Mappers.League;

public static class LeagueInstanceMapper
{
    public static AppLeague.LeagueInstance Map(this DatabaseLeague.LeagueInstance leagueInstance) =>
        TrackedModelMapper.MapToAppModel(leagueInstance, new AppLeague.LeagueInstance
        {
            ExternalId = leagueInstance.ExternalId,
            LeagueId = leagueInstance.LeagueId,
            SeasonId = leagueInstance.SeasonId,
            LeagueSlots = [.. leagueInstance.LeagueSlots.Select(x => x.Map())],
            FantasyTeams = [.. leagueInstance.FantasyTeams.Select(x => x.Map())],
            FantasyScores = [.. leagueInstance.FantasyScores.Select(x => x.Map())],
            PlayerSeasonStats = [.. leagueInstance.PlayerSeasonStats.Select(x => x.Map())],
        });

    public static DatabaseLeague.LeagueInstance Map(this AppLeague.LeagueInstance leagueInstance) =>
        TrackedModelMapper.MapToDbModel(leagueInstance, new DatabaseLeague.LeagueInstance
        {
            ExternalId = leagueInstance.ExternalId,
            LeagueId = leagueInstance.LeagueId,
            SeasonId = leagueInstance.SeasonId,
            LeagueSlots = [.. leagueInstance.LeagueSlots.Select(x => x.Map())],
            FantasyTeams = [.. leagueInstance.FantasyTeams.Select(x => x.Map())],
            FantasyScores = [.. leagueInstance.FantasyScores.Select(x => x.Map())],
            PlayerSeasonStats = [.. leagueInstance.PlayerSeasonStats.Select(x => x.Map())],
        });
}
