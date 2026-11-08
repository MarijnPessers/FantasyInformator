using System.Collections;
using System.Reflection;
using DatabaseTracking = FantasyInformator.Database.Models.Tracking;

namespace FantasyInformator.Database.Tests;

/// <summary>
/// Checks a mapped application model against the database model it came from, by reflection, so a field that a mapper
/// forgets makes the test fail. It checks every own field of the model and of each child in its lists (one level deep, like
/// <see cref="SourceFactory"/>). Enums are compared by name.
/// </summary>
public static class MappedAssert
{
    private static readonly string[] DatabaseOnlyFields =
    [
        nameof(DatabaseTracking.TrackedModel.CreatedUtc),
        nameof(DatabaseTracking.TrackedModel.UpdatedUtc),
    ];

    public static void Equal(object databaseModel, object model)
    {
        OwnFieldsEqual(databaseModel, model);

        foreach (var childList in SourceFactory.GetChildLists(databaseModel.GetType()))
        {
            var databaseChildren = (IList)childList.GetValue(databaseModel)!;
            var children = (IList)model.GetType().GetProperty(childList.Name)!.GetValue(model)!;

            Assert.HasCount(databaseChildren.Count, children, $"Number of elements in {childList.Name}");

            for (var elementNumber = 0; elementNumber < databaseChildren.Count; elementNumber++)
            {
                OwnFieldsEqual(databaseChildren[elementNumber]!, children[elementNumber]!);
            }
        }
    }

    public static void OwnFieldsEqual(object databaseModel, object model)
    {
        List<PropertyInfo> databaseFields =
        [
            .. SourceFactory.GetOwnFields(databaseModel.GetType()).Where(x => !DatabaseOnlyFields.Contains(x.Name)),
        ];
        Assert.IsNotEmpty(databaseFields);

        foreach (var databaseField in databaseFields)
        {
            var field = model.GetType().GetProperty(databaseField.Name);
            Assert.IsNotNull(field, $"{databaseField.Name} exists in the model");

            var databaseValue = databaseField.GetValue(databaseModel);
            var value = field.GetValue(model);

            if (databaseField.PropertyType.IsEnum)
            {
                Assert.AreEqual(databaseValue!.ToString(), value!.ToString(), databaseField.Name);
            }
            else
            {
                Assert.AreEqual(databaseValue, value, databaseField.Name);
            }
        }
    }
}
