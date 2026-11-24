using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Models.FantasyTeams;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeams;

[TestClass]
public class RosteredPlayerMapperTests
{
    private RosteredPlayerMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new RosteredPlayerMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var rosteredPlayer = SourceFactory.Create<RosteredPlayerDb>();

        var result = _mapper.Map(rosteredPlayer);

        Assert.IsNotNull(result);
        MappedAssert.Equal(rosteredPlayer, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<RosteredPlayerDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
