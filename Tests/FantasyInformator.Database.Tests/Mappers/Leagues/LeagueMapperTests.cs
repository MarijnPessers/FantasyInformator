using FantasyInformator.Database.Mappers.Leagues;
using FantasyInformator.Database.Models.Leagues;

namespace FantasyInformator.Database.Tests.Mappers.Leagues;

[TestClass]
public class LeagueMapperTests
{
    private LeagueMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new LeagueMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var league = SourceFactory.Create<LeagueDb>();

        var result = _mapper.Map(league);

        Assert.IsNotNull(result);
        MappedAssert.Equal(league, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<LeagueDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
