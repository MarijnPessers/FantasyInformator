using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Models.FantasyTeams;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeams;

[TestClass]
public class FantasyTeamMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var fantasyTeam = SourceFactory.Create<FantasyTeamDb>();

        var result = FantasyTeamMapper.Map(fantasyTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyTeam, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<FantasyTeamDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
