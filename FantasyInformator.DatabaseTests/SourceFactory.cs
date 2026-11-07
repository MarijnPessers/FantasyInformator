using System.Collections;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;

namespace FantasyInformator.DatabaseTests;

/// <summary>
/// Builds a database model one level deep: the own fields get random values, every link to another database model
/// gets an object with random own fields, and every list gets <see cref="ListElementCount"/> such objects.
/// The links of those objects stay empty, so the source never has loops and stays small.
/// </summary>
public static class SourceFactory
{
    public const int ListElementCount = 3;

    private const string DatabaseModelsNamespace = "FantasyInformator.Database.Models";

    private static readonly SpecimenContext SpecimenContext = new(new Fixture());

    public static TModel Create<TModel>()
        where TModel : class
    {
        var databaseModel = CreateWithOwnFields(typeof(TModel));

        foreach (var link in GetLinks(typeof(TModel)))
        {
            link.SetValue(databaseModel, CreateLink(link.PropertyType));
        }

        return (TModel)databaseModel;
    }

    public static IEnumerable<PropertyInfo> GetLinks(Type modelType) =>
        modelType.GetProperties().Where(IsLink);

    public static IEnumerable<PropertyInfo> GetOwnFields(Type modelType) =>
        modelType.GetProperties().Where(x => !IsLink(x));

    private static bool IsLink(PropertyInfo property) =>
        IsDatabaseModel(property.PropertyType)
        || (IsList(property.PropertyType) && IsDatabaseModel(property.PropertyType.GetGenericArguments()[0]));

    private static bool IsList(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);

    private static bool IsDatabaseModel(Type type) =>
        type.Namespace is not null
        && type.Namespace.StartsWith(DatabaseModelsNamespace, StringComparison.Ordinal)
        && type.IsClass;

    private static object CreateLink(Type linkType)
    {
        if (!IsList(linkType))
        {
            return CreateWithOwnFields(linkType);
        }

        var list = (IList)Activator.CreateInstance(linkType)!;

        for (var elementNumber = 0; elementNumber < ListElementCount; elementNumber++)
        {
            _ = list.Add(CreateWithOwnFields(linkType.GetGenericArguments()[0]));
        }

        return list;
    }

    private static object CreateWithOwnFields(Type modelType)
    {
        var databaseModel = Activator.CreateInstance(modelType)!;

        foreach (var ownField in GetOwnFields(modelType))
        {
            ownField.SetValue(databaseModel, SpecimenContext.Resolve(ownField.PropertyType));
        }

        return databaseModel;
    }
}
