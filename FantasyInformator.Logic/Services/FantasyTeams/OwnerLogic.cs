using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Logic.Services.FantasyTeams;

public interface IOwnerLogic
{
    /// <summary>Returns all owners from the database, without their teams. Null when that fails.</summary>
    Task<List<Owner>?> LoadAllOwnersAsync(CancellationToken cancellationToken);
}

public class OwnerLogic(IDatabaseOwnerService databaseOwnerService) : IOwnerLogic
{
    public async Task<List<Owner>?> LoadAllOwnersAsync(CancellationToken cancellationToken) =>
        [.. await databaseOwnerService.GetAll(cancellationToken)];
}
