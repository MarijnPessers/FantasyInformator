using FantasyInformator.Database.Models;
using FantasyInformator.Domain;
using FantasyInformator.Shared.Mappers;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// <see cref="MapContext"/> does not know which properties are links, or what every copy gets besides its own fields.
/// For copying between a model and its database model: a property is a link when it holds a model or a database model,
/// every copy gets the Id of its original, and a model also gets the state Unchanged.
/// Example: PlayerDb's CurrentNflTeam holds an NflTeamDb, so it is a link, and the Player copy gets the Id of the
/// PlayerDb.
/// </summary>
internal sealed class EntityMapContext : MapContext
{
    /// <summary>True for a model or a database model.</summary>
    protected override bool IsLink(Type type) =>
        typeof(BaseEntity).IsAssignableFrom(type) || typeof(BaseEntityDb).IsAssignableFrom(type);

    /// <inheritdoc/>
    protected override void OnCopied(object source, object target)
    {
        if (source is BaseEntityDb sourceDatabaseModel && target is BaseEntity targetModel)
        {
            _ = targetModel.MapBase(sourceDatabaseModel);
        }
        else if (source is BaseEntity sourceModel && target is BaseEntityDb targetDatabaseModel)
        {
            _ = targetDatabaseModel.MapBase(sourceModel);
        }
        else
        {
            // Any other pair gets only its own fields.
        }
    }
}
