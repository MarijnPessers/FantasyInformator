using System.Reflection;
using FantasyInformator.Database.Mappers;
using FantasyInformator.Database.Models;
using FantasyInformator.Database.Models.Nfl;
using FantasyInformator.Domain;
using FantasyInformator.Domain.Nfl;
using FantasyInformator.Domain.Players;
using FantasyInformator.Shared.Mappers;

namespace FantasyInformator.Database.Tests.Mappers;

/// <summary>
/// Reading and saving find their mapper by the two models it copies between while the program runs. A missing one
/// would only fail the first time that model is read or saved, so these tests check every model at once.
/// Example: reading Players needs a mapper that copies a PlayerDb into a Player, and saving Players needs a mapper that
/// copies a Player into a PlayerDb.
/// </summary>
[TestClass]
public class MapperLookupTests
{
    private static readonly Type[] Models =
    [
        .. typeof(BaseEntity).Assembly.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false } && x.IsAssignableTo(typeof(BaseEntity))),
    ];

    private static readonly MethodInfo FindMethodInfo =
        typeof(MapperLookup).GetMethod(nameof(MapperLookup.Find), 2, [typeof(Assembly)])!;

    [TestMethod]
    public void Find_ShouldFindOneMapper_WhenReadingAnyModel()
    {
        var modelsWithoutMapper = Models
            .Where(model => TryFindDatabaseModel(model) is { } databaseModel && !HasOneMapper(databaseModel, model))
            .Select(model => model.Name)
            .ToList();

        Assert.IsEmpty(modelsWithoutMapper, string.Join(", ", modelsWithoutMapper));
    }

    [TestMethod]
    public void Find_ShouldFindOneMapper_WhenSavingAnyModel()
    {
        var modelsWithoutMapper = Models
            .Where(model => TryFindDatabaseModel(model) is { } databaseModel && !HasOneMapper(model, databaseModel))
            .Select(model => model.Name)
            .ToList();

        Assert.IsEmpty(modelsWithoutMapper, string.Join(", ", modelsWithoutMapper));
    }

    [TestMethod]
    public void Find_ShouldGiveMapperThatCopies_WhenModelsAreNamedInTurnedAroundOrder()
    {
        var season = SourceFactory.Create<SeasonDb>();

        var result = MapperLookup.Find<SeasonDb, Season>(typeof(EntityMapContext).Assembly).Map(season);

        MappedAssert.Equal(season, result);
    }

    [TestMethod]
    public void Find_ShouldThrowMappingException_WhenNoMapperCopiesBetweenModels()
    {
        var exception = Assert.ThrowsExactly<MappingException>(
            () => MapperLookup.Find<Season, Player>(typeof(EntityMapContext).Assembly));

        Assert.AreEqual(typeof(Season), exception.SourceType);
        Assert.AreEqual(typeof(Player), exception.TargetType);
    }

    // A model without exactly one database model already fails DatabaseModelTypesTests, so it is skipped here.
    private static Type? TryFindDatabaseModel(Type model)
    {
        try
        {
            return DatabaseModelTypes.Find(model);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    // Find throws when it finds none or more than one. That is caught here, so the test can list every model that
    // fails instead of stopping at the first one. The models are only known while the test runs, so Find is set to
    // them through FindMethodInfo.
    private static bool HasOneMapper(Type sourceType, Type targetType)
    {
        try
        {
            _ = FindMethodInfo
                .MakeGenericMethod(sourceType, targetType)
                .CreateDelegate<Func<Assembly, IMapper>>()(typeof(EntityMapContext).Assembly);
            return true;
        }
        catch (MappingException)
        {
            return false;
        }
    }
}
