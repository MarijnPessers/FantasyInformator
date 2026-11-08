using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.DatabaseTests.Mappers.FantasyTeam;

[TestClass]
public class RosteredPlayerMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var rosteredPlayer = SourceFactory.Create<DatabaseFantasyTeam.RosteredPlayer>();

        var result = RosteredPlayerMapper.Map(rosteredPlayer);

        Assert.IsNotNull(result);
        MappedAssert.Equal(rosteredPlayer, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = RosteredPlayerMapper.Map(null);

        Assert.IsNull(result);
    }
}
