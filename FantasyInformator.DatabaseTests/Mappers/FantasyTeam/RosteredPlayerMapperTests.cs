using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.DatabaseTests.Mappers.FantasyTeam;

[TestClass]
public class RosteredPlayerMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var rosteredPlayer = SourceFactory.Create<DatabaseFantasyTeam.RosteredPlayer>();

        var result = RosteredPlayerMapper.ToModel(rosteredPlayer);

        Assert.IsNotNull(result);
        MappedAssert.Equal(rosteredPlayer, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = RosteredPlayerMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
