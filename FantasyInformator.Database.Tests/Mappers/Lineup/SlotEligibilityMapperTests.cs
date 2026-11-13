using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Tests.Mappers.Lineup;

[TestClass]
public class SlotEligibilityMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var slotEligibility = SourceFactory.Create<DatabaseLineup.SlotEligibility>();

        var result = SlotEligibilityMapper.Map(slotEligibility);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slotEligibility, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<DatabaseLineup.SlotEligibility>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
