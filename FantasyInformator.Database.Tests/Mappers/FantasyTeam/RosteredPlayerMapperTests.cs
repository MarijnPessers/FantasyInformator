using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeam;

[TestClass]
public class RosteredPlayerMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var rosteredPlayer = SourceFactory.Create<DatabaseFantasyTeam.RosteredPlayer>();

        var result = RosteredPlayerMapper.Map(rosteredPlayer);

        Assert.IsNotNull(result);
        MappedAssert.Equal(rosteredPlayer, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseFantasyTeam.RosteredPlayer>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
