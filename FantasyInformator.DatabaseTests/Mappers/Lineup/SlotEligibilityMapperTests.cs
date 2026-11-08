using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.DatabaseTests.Mappers.Lineup;

[TestClass]
public class SlotEligibilityMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var slotEligibility = SourceFactory.Create<DatabaseLineup.SlotEligibility>();

        var result = SlotEligibilityMapper.Map(slotEligibility);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slotEligibility, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = SlotEligibilityMapper.Map(null);

        Assert.IsNull(result);
    }
}
