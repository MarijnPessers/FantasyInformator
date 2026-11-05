using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class SlotEligibilityMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var slotEligibility = SourceFactory.Create<DatabaseModels.SlotEligibility>();

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
