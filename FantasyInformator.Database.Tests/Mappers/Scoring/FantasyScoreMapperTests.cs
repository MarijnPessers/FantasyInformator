using FantasyInformator.Database.Mappers.Scoring;
using DatabaseScoring = FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Tests.Mappers.Scoring;

[TestClass]
public class FantasyScoreMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var fantasyScore = SourceFactory.Create<DatabaseScoring.FantasyScore>();

        var result = FantasyScoreMapper.Map(fantasyScore);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyScore, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<DatabaseScoring.FantasyScore>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
