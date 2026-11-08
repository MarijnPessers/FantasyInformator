using FantasyInformator.Database.Mappers.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Tests.Mappers.League;

[TestClass]
public class LeagueInstanceMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var leagueInstance = SourceFactory.Create<DatabaseLeague.LeagueInstance>();

        var result = LeagueInstanceMapper.Map(leagueInstance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueInstance, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseLeague.LeagueInstance>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
