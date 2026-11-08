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
        var result = SlotMapper.Map((DatabaseLineup.Slot?)null);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllOwnFields()
    {
        var model = SourceFactory.Create<DatabaseLineup.Slot>().Map()!;

        var result = model.Map();

        MappedAssert.OwnFieldsEqual(result, model);
    }
}
