using FantasyInformator.Database.Mappers.Leagues;
using FantasyInformator.Database.Models.Leagues;

namespace FantasyInformator.Database.Tests.Mappers.Leagues;

[TestClass]
public class LeagueInstanceMapperTests
{
    private LeagueInstanceMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new LeagueInstanceMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var leagueInstance = SourceFactory.Create<LeagueInstanceDb>();

        var result = _mapper.Map(leagueInstance);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueInstance, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<LeagueInstanceDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
