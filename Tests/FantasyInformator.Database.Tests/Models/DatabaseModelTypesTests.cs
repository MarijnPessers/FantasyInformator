using FantasyInformator.Database.Models;
using FantasyInformator.Domain;

namespace FantasyInformator.Database.Tests.Models;

/// <summary>
/// Reading and saving find the database model of a model by name while the program runs. A missing one would only
/// fail the first time that model is read or saved, so this test checks every model at once.
/// Example: Player needs a PlayerDb.
/// </summary>
[TestClass]
public class DatabaseModelTypesTests
{
    private static readonly Type[] Models =
    [
        .. typeof(BaseEntity).Assembly.GetTypes()
            .Where(x => x is { IsClass: true, IsAbstract: false } && x.IsAssignableTo(typeof(BaseEntity))),
    ];

    [TestMethod]
    public void Find_ShouldFindOneDatabaseModel_WhenAnyModelIsGiven()
    {
        var modelsWithout = Models
            .Where(model => TryFind(model) is null)
            .Select(model => model.Name)
            .ToList();

        Assert.IsEmpty(modelsWithout, string.Join(", ", modelsWithout));
    }

    // Find throws when it finds none or more than one. That is caught here, so the test can list every model that
    // fails instead of stopping at the first one.
    private static Type? TryFind(Type model)
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
}
