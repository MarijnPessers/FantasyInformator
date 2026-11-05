using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class OwnerMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var owner = SourceFactory.Create<DatabaseModels.Owner>();

        var result = OwnerMapper.ToModel(owner);

        Assert.IsNotNull(result);
        MappedAssert.Equal(owner, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = OwnerMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
