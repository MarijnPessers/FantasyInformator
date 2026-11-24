using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class NflTeamMapperTests
{
    private NflTeamMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new NflTeamMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var nflTeam = SourceFactory.Create<NflTeamDb>();

        var result = _mapper.Map(nflTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(nflTeam, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<NflTeamDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
