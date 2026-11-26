namespace FantasyInformator.Logic.Services.Databases;

public interface IDatabaseWakeUpLogic
{
    /// <summary>Wakes the database. False when it doesn't answer.</summary>
    Task<bool> WakeUpAsync(CancellationToken cancellationToken);
}

public class DatabaseWakeUpLogic(IDatabaseWakeUpService databaseWakeUpService) : IDatabaseWakeUpLogic
{
    public Task<bool> WakeUpAsync(CancellationToken cancellationToken) =>
        databaseWakeUpService.WakeUpAsync(cancellationToken);
}
