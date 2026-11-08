using FantasyInformator.Database.Mappers.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.DatabaseTests.Mappers.League;

[TestClass]
public class LeagueInstanceMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var leagueInstance = SourceFactory.Create<DatabaseLeague.LeagueInstance>();

        var result = LeagueInstanceMapper.Map(leagueInstance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueInstance, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = LeagueInstanceMapper.Map((DatabaseLeague.LeagueInstance?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabaseLeague.LeagueInstance>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
