using FantasyInformator.Database.Mappers.Leagues;
using FantasyInformator.Database.Models.Leagues;

namespace FantasyInformator.Database.Tests.Mappers.Leagues;

[TestClass]
public class LeagueMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var league = SourceFactory.Create<LeagueDb>();

        var result = LeagueMapper.Map(league);

        Assert.IsNotNull(result);
        MappedAssert.Equal(league, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<LeagueDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
