using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class LeagueInstanceMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var leagueInstance = SourceFactory.Create<DatabaseModels.LeagueInstance>();

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
