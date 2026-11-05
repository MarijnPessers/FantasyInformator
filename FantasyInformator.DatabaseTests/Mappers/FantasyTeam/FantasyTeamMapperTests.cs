using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class FantasyTeamMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var fantasyTeam = SourceFactory.Create<DatabaseModels.FantasyTeam>();

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
