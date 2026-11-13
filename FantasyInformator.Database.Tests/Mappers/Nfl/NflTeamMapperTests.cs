using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class NflTeamMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var nflTeam = SourceFactory.Create<DatabaseNfl.NflTeam>();

        var result = NflTeamMapper.Map(nflTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(nflTeam, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<DatabaseNfl.NflTeam>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
