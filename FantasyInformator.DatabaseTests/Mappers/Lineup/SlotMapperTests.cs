using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.DatabaseTests.Mappers.Lineup;

[TestClass]
public class SlotMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndLinks()
    {
        var slot = SourceFactory.Create<DatabaseLineup.Slot>();

        var result = SlotMapper.Map(slot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slot, result);
    }

    [TestMethod]
    public void Map_Null_ReturnsNull()
    {
        var result = SlotMapper.Map(null);

        Assert.IsNull(result);
    }
}
