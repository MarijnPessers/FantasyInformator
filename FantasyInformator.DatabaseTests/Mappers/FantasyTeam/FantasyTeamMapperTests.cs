using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.DatabaseTests.Mappers.FantasyTeam;

[TestClass]
public class FantasyTeamMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var fantasyTeam = SourceFactory.Create<DatabaseFantasyTeam.FantasyTeam>();

        var result = FantasyTeamMapper.Map(fantasyTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyTeam, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = FantasyTeamMapper.Map((DatabaseFantasyTeam.FantasyTeam?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabaseFantasyTeam.FantasyTeam>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
