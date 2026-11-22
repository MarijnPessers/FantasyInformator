using FantasyInformator.Database.Mappers.Scoring;
using FantasyInformator.Database.Models.Scoring;

namespace FantasyInformator.Database.Tests.Mappers.Scoring;

[TestClass]
public class FantasyScoreMapperTests
{
    private FantasyScoreMapper _mapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = new FantasyScoreMapper();
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var fantasyScore = SourceFactory.Create<FantasyScoreDb>();

        var result = _mapper.Map(fantasyScore);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyScore, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<FantasyScoreDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
