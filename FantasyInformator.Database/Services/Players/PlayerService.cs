using FantasyInformator.Domain.Players;
using FantasyInformator.Shared.Database;

namespace FantasyInformator.Database.Services.Players;

/// <summary>What Logic can read about players.</summary>
public interface IPlayerService : IEntityService<Player>;

internal class PlayerService(IDatabaseReadService databaseReadService)
    : EntityService<Player>(databaseReadService), IPlayerService;
