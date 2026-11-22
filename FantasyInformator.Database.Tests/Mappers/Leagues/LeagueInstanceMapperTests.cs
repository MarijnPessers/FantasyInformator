using FantasyInformator.Database.Mappers.Leagues;
using FantasyInformator.Database.Models.Leagues;

namespace FantasyInformator.Database.Tests.Mappers.Leagues;

[TestClass]
public class LeagueInstanceMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var leagueInstance = SourceFactory.Create<LeagueInstanceDb>();

        var result = LeagueInstanceMapper.Map(leagueInstance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueInstance, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<LeagueInstanceDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
