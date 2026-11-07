using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.DatabaseTests.Mappers.Lineup;

[TestClass]
public class SlotMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var slot = SourceFactory.Create<DatabaseLineup.Slot>();

        var result = SlotMapper.ToModel(slot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slot, result);
    }

    [TestMethod]
    public void ToModel_Null_ReturnsNull()
    {
        var result = SlotMapper.ToModel(null);

        Assert.IsNull(result);
    }
}
