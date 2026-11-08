using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeam;

[TestClass]
public class FantasyTeamMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var fantasyTeam = SourceFactory.Create<DatabaseFantasyTeam.FantasyTeam>();

        var result = FantasyTeamMapper.Map(fantasyTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyTeam, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseFantasyTeam.FantasyTeam>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
