using FantasyInformator.Database.Mappers;
using FantasyInformator.Database.Models;
using FantasyInformator.Domain;

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
    // fails instead of stopping at the first one.
    private static bool HasOneMapper(Type sourceType, Type targetType)
    {
        try
        {
            _ = MapperLookup.Find(sourceType, targetType);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
}
