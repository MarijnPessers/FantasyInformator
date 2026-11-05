using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class LeagueSlotMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var leagueSlot = SourceFactory.Create<DatabaseModels.LeagueSlot>();

        var result = LeagueSlotMapper.ToModel(leagueSlot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueSlot, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = LeagueSlotMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
