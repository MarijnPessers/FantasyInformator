using FantasyInformator.Database.Mappers.Nfl;
using DatabaseNfl = FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.DatabaseTests.Mappers.Nfl;

[TestClass]
public class NflTeamMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var nflTeam = SourceFactory.Create<DatabaseNfl.NflTeam>();

        var result = NflTeamMapper.Map(nflTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(nflTeam, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = NflTeamMapper.Map((DatabaseNfl.NflTeam?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabaseNfl.NflTeam>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
