using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class NflTeamMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var nflTeam = SourceFactory.Create<DatabaseModels.NflTeam>();

        var result = NflTeamMapper.ToModel(nflTeam);

        Assert.IsNotNull(result);
        MappedAssert.Equal(nflTeam, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = NflTeamMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
