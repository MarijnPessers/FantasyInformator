using FantasyInformator.Database.Mappers.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.DatabaseTests.Mappers.Scoring;

[TestClass]
public class FantasyScoreMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var fantasyScore = SourceFactory.Create<DatabaseScoring.FantasyScore>();

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
