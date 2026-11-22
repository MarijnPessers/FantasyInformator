using FantasyInformator.Domain.Players;

namespace FantasyInformator.Database.Services.Players;

/// <summary>What Logic can read about players.</summary>
public interface IPlayerService : IEntityService<Player>;

internal class PlayerService(IDatabaseReadService databaseReadService)
    : EntityService<Player>(databaseReadService), IPlayerService;
