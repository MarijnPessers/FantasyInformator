using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Mappers.Lineups;
using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Mappers.Tracking;
using FantasyInformator.Database.Models.Leagues;
using AppLeague = FantasyInformator.Domain.Leagues;

namespace FantasyInformator.Database.Mappers.Leagues;

public static class LeagueInstanceMapper
{
    public static AppLeague.LeagueInstance Map(this LeagueInstanceDb leagueInstance) =>
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

    public static LeagueInstanceDb Map(this AppLeague.LeagueInstance leagueInstance) =>
        TrackedModelMapper.Map(leagueInstance, new LeagueInstanceDb
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
