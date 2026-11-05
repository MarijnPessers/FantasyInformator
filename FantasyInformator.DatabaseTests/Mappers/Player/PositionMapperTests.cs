using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class PositionMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var position = SourceFactory.Create<DatabaseModels.Position>();

        var result = PositionMapper.ToModel(position);

        Assert.IsNotNull(result);
        MappedAssert.Equal(position, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = PositionMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
