using System.Collections;
using System.Reflection;

namespace FantasyInformator.DatabaseTests;

/// <summary>
/// Checks a mapped application model against the database model it came from, by reflection, so a field that a mapper
/// forgets makes the test fail. It checks every own field of the model and of each linked model (one level deep, like
/// <see cref="SourceFactory"/>). Enums are compared by name.
/// </summary>
public static class MappedAssert
{
    public static void Equal(object databaseModel, object model)
    {
        OwnFieldsEqual(databaseModel, model);

        foreach (var link in SourceFactory.GetLinks(databaseModel.GetType()))
        {
            var databaseLink = link.GetValue(databaseModel);
            var modelLink = model.GetType().GetProperty(link.Name)!.GetValue(model);

            if (databaseLink is IList databaseList)
            {
                var modelList = (IList)modelLink!;
                Assert.HasCount(databaseList.Count, modelList, $"Number of elements in {link.Name}");

                for (var elementNumber = 0; elementNumber < databaseList.Count; elementNumber++)
                {
                    OwnFieldsEqual(databaseList[elementNumber]!, modelList[elementNumber]!);
                }
            }
            else
            {
                Assert.IsNotNull(modelLink, $"{link.Name} is mapped");
                OwnFieldsEqual(databaseLink!, modelLink);
            }
        }
    }

    private static void OwnFieldsEqual(object databaseModel, object model)
    {
        List<PropertyInfo> ownFields = [.. SourceFactory.GetOwnFields(model.GetType())];
        Assert.IsNotEmpty(ownFields);

        foreach (var ownField in ownFields)
        {
            var databaseField = databaseModel.GetType().GetProperty(ownField.Name);
            Assert.IsNotNull(databaseField, $"{ownField.Name} exists in the database model");

            var databaseValue = databaseField.GetValue(databaseModel);
            var value = ownField.GetValue(model);

            if (ownField.PropertyType.IsEnum)
            {
                Assert.AreEqual(databaseValue!.ToString(), value!.ToString(), ownField.Name);
            }
            else
            {
                Assert.AreEqual(databaseValue, value, ownField.Name);
            }
        }
    }
}
