using FantasyInformator.Database.Mappers.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.DatabaseTests.Mappers.League;

[TestClass]
public class LeagueInstanceMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var leagueInstance = SourceFactory.Create<DatabaseLeague.LeagueInstance>();

        var result = LeagueInstanceMapper.ToModel(leagueInstance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueInstance, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = LeagueInstanceMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
