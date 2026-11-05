using System.Diagnostics.CodeAnalysis;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// Remembers which database object was mapped to which application object during one mapping.
/// The models link to each other in both directions (a league holds its league instances and each one holds its
/// league), so without it a mapping would never end. It also makes one database object give one application object.
/// </summary>
public sealed class MappingContext
{
    private readonly Dictionary<object, object> _mappedByDatabaseModel = new(ReferenceEqualityComparer.Instance);

    public bool TryGetMapped<TModel>(object databaseModel, [NotNullWhen(true)] out TModel? mapped)
        where TModel : class
    {
        if (_mappedByDatabaseModel.TryGetValue(databaseModel, out var existing))
        {
            mapped = (TModel)existing;
            return true;
        }

        mapped = null;
        return false;
    }

    /// <summary>
    /// Registers the application object before its links are mapped, so a link that leads back to it finds it.
    /// </summary>
    public void Register(object databaseModel, object model) => _mappedByDatabaseModel.Add(databaseModel, model);

    /// <summary>Maps every element with the given mapper, using this context.</summary>
    public List<TModel> MapList<TDatabaseModel, TModel>(
        IEnumerable<TDatabaseModel> databaseModels,
        Func<TDatabaseModel, MappingContext, TModel?> map)
        where TModel : class
    {
        return [.. databaseModels.Select(databaseModel => map(databaseModel, this)).OfType<TModel>()];
    }
}
