using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Models.FantasyTeams;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeams;

[TestClass]
public class FantasyTeamMapperTests
{
    private FantasyTeamMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new FantasyTeamMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var fantasyTeam = SourceFactory.Create<FantasyTeamDb>();

        var result = _mapper.Map(fantasyTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(fantasyTeam, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<FantasyTeamDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
