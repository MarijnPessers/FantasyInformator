using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Models.FantasyTeams;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeams;

[TestClass]
public class RosteredPlayerMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var rosteredPlayer = SourceFactory.Create<RosteredPlayerDb>();

        var result = RosteredPlayerMapper.Map(rosteredPlayer);

        Assert.IsNotNull(result);
        MappedAssert.Equal(rosteredPlayer, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<RosteredPlayerDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
