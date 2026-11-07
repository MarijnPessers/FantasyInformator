using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.DatabaseTests.Mappers.Nfl;

[TestClass]
public class NflTeamMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var nflTeam = SourceFactory.Create<DatabaseNfl.NflTeam>();

        var result = NflTeamMapper.ToModel(nflTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(nflTeam, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = NflTeamMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
