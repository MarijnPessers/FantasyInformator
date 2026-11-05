using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class FantasyScoreMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var fantasyScore = SourceFactory.Create<DatabaseModels.FantasyScore>();

        var result = FantasyScoreMapper.ToModel(fantasyScore);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyScore, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = FantasyScoreMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
