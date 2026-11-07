using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.DatabaseTests.Mappers.FantasyTeam;

[TestClass]
public class FantasyTeamMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var fantasyTeam = SourceFactory.Create<DatabaseFantasyTeam.FantasyTeam>();

        var result = FantasyTeamMapper.ToModel(fantasyTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyTeam, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = FantasyTeamMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
