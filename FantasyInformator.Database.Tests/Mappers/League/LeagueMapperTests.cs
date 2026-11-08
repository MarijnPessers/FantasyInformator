using FantasyInformator.Database.Mappers.League;
using DatabaseLeague = FantasyInformator.Database.Models.League;

namespace FantasyInformator.Database.Tests.Mappers.League;

[TestClass]
public class LeagueMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var league = SourceFactory.Create<DatabaseLeague.League>();

        var result = LeagueMapper.Map(league);

        Assert.IsNotNull(result);
        MappedAssert.Equal(league, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseLeague.League>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
