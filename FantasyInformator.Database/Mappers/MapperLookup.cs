using System.Collections.Concurrent;
using FantasyInformator.Database.Models;
using FantasyInformator.Domain;

namespace FantasyInformator.Database.Mappers;

/// <summary>
/// <see cref="Services.DatabaseReadService"/>, <see cref="Services.DatabaseWriteService"/> and <see cref="MapContext"/>
/// only know the two models they copy between, not the mapper class that does the copying. This class finds the mapper
/// from those two, so they never have to name it.
/// Example: a PlayerDb and a Player give PlayerMapper.
/// </summary>
internal static class MapperLookup
{
    /// <summary>
    /// A list of mappers, one per pair of what is copied and what it is copied into.
    /// Finding a mapper means checking every mapper class, so each one is found once and kept here.
    /// Example: PlayerMapper, for copying a PlayerDb into a Player.
    /// </summary>
    private static readonly ConcurrentDictionary<(Type Source, Type Target), IMapper> Mappers = new();

    /// <summary>
    /// The mapper that copies between TModel and TDatabaseModel, as its own type, so its Map methods can be called
    /// directly. Throws when there is none or more than one.
    /// Example: for Player and PlayerDb it gives PlayerMapper.
    /// </summary>
    public static Mapper<TModel, TDatabaseModel> Find<TModel, TDatabaseModel>()
        where TModel : BaseEntity
        where TDatabaseModel : BaseEntityDb =>
        (Mapper<TModel, TDatabaseModel>)Find(typeof(TModel), typeof(TDatabaseModel));

    /// <summary>
    /// Every pair of model and database model has its own mapper class, which copies both ways. So this method looks
    /// through all classes in <see cref="MapperTypes"/> for the one that copies between <paramref name="sourceType"/>
    /// and <paramref name="targetType"/>, in either direction, and gives back an object of it. It throws when it finds
    /// none or more than one, because then a mapper is missing or written twice.
    /// Example: a PlayerDb and a Player give PlayerMapper, and so do a Player and a PlayerDb.
    /// </summary>
    public static IMapper Find(Type sourceType, Type targetType) =>
        Mappers.GetOrAdd(
            (sourceType, targetType),
            key => (IMapper)Activator.CreateInstance(
                MapperTypes.All.Single(x => CopiesBetween(x, key.Source, key.Target)))!);

    // A mapper class derives from Mapper with its model first and its database model second. The source and the target
    // can be either one, so both orders are checked.
    // Example: PlayerMapper derives from Mapper with Player and PlayerDb, so it copies a PlayerDb into a Player and a
    // Player into a PlayerDb.
    private static bool CopiesBetween(Type mapperType, Type sourceType, Type targetType) =>
        mapperType.BaseType!.GetGenericArguments() is [var model, var databaseModel]
        && ((sourceType == model && targetType == databaseModel)
            || (sourceType == databaseModel && targetType == model));
}
