using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.DatabaseTests.Mappers.Lineup;

[TestClass]
public class SlotEligibilityMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var slotEligibility = SourceFactory.Create<DatabaseLineup.SlotEligibility>();

        var result = SlotEligibilityMapper.ToModel(slotEligibility);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slotEligibility, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = SlotEligibilityMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
