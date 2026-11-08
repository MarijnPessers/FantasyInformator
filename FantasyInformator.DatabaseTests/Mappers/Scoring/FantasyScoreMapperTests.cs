using FantasyInformator.Database.Mappers.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.DatabaseTests.Mappers.Scoring;

[TestClass]
public class FantasyScoreMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var fantasyScore = SourceFactory.Create<DatabaseScoring.FantasyScore>();

        var result = FantasyScoreMapper.Map(fantasyScore);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyScore, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = FantasyScoreMapper.Map(null);

        Assert.IsNull(result);
    }
}
