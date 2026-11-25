using FantasyInformator.Database.Services.FantasyTeams;
using FantasyInformator.Domain.FantasyTeams;

namespace FantasyInformator.Logic.Services.FantasyTeams;

public interface IOwnerLogic
{
    /// <summary>Returns all owners from the database, without their teams. Null when that fails.</summary>
    Task<List<Owner>?> LoadAllOwnersAsync(CancellationToken cancellationToken);
}

public class OwnerLogic(IOwnerService ownerService) : IOwnerLogic
{
    public async Task<List<Owner>?> LoadAllOwnersAsync(CancellationToken cancellationToken) =>
        [.. await ownerService.GetAll(cancellationToken)];
}
