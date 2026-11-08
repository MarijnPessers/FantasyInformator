using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Tests.Mappers.Lineup;

[TestClass]
public class SlotEligibilityMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var slotEligibility = SourceFactory.Create<DatabaseLineup.SlotEligibility>();

        var result = SlotEligibilityMapper.Map(slotEligibility);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slotEligibility, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseLineup.SlotEligibility>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
