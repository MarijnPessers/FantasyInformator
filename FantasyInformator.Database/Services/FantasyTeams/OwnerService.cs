using FantasyInformator.Domain.FantasyTeams;
using FantasyInformator.Shared.Database;

namespace FantasyInformator.Database.Services.FantasyTeams;

/// <summary>What Logic can read about owners.</summary>
public interface IOwnerService : IEntityService<Owner>;

internal class OwnerService(IDatabaseReadService databaseReadService)
    : EntityService<Owner>(databaseReadService), IOwnerService;
