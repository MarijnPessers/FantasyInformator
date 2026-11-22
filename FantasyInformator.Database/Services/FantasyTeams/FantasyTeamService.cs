using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Database.Services.FantasyTeams;

/// <summary>What Logic can read about fantasy teams.</summary>
public interface IFantasyTeamService : IEntityService<FantasyTeam>;

internal class FantasyTeamService(IDatabaseReadService databaseReadService)
    : EntityService<FantasyTeam>(databaseReadService), IFantasyTeamService;
