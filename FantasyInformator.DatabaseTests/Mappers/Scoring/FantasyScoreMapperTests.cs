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
        var result = FantasyScoreMapper.Map((DatabaseScoring.FantasyScore?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabaseScoring.FantasyScore>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
