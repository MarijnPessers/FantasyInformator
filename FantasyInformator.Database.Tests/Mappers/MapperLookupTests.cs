using FantasyInformator.Database.Mappers;
using FantasyInformator.Database.Models;
using FantasyInformator.Domain;

namespace FantasyInformator.Database.Tests.Mappers;

/// <summary>
/// Reading and saving find their Map method by what goes in and what comes out while the program runs. A missing one
/// would only fail the first time that model is read or saved, so these tests check every model at once.
/// Example: reading Players needs a Map method from a list of PlayerDb to a list of Players, and saving Players needs a
/// Map method from a Player to a PlayerDb.
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
    public void Find_ShouldFindOneMapMethod_WhenReadingAnyModel()
    {
        var modelsWithout = Models
            .Where(model => TryFindDatabaseModel(model) is { } databaseModel
                && !HasOneMapMethod(
                    typeof(IEnumerable<>).MakeGenericType(databaseModel),
                    typeof(IEnumerable<>).MakeGenericType(model)))
            .Select(model => model.Name)
            .ToList();

        Assert.IsEmpty(modelsWithout, string.Join(", ", modelsWithout));
    }

    [TestMethod]
    public void Find_ShouldFindOneMapMethod_WhenSavingAnyModel()
    {
        var modelsWithout = Models
            .Where(model => TryFindDatabaseModel(model) is { } databaseModel && !HasOneMapMethod(model, databaseModel))
            .Select(model => model.Name)
            .ToList();

        Assert.IsEmpty(modelsWithout, string.Join(", ", modelsWithout));
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
    private static bool HasOneMapMethod(Type input, Type output)
    {
        try
        {
            _ = MapperLookup.Find([input], output);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
}
