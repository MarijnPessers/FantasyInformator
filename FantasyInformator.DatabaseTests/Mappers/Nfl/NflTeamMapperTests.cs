using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.DatabaseTests.Mappers.Nfl;

[TestClass]
public class NflTeamMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var nflTeam = SourceFactory.Create<DatabaseNfl.NflTeam>();

        var result = NflTeamMapper.Map(nflTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(nflTeam, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = NflTeamMapper.Map(null);

        Assert.IsNull(result);
    }
}
