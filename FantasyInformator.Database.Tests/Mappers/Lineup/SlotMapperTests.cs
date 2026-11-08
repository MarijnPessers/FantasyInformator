using FantasyInformator.Database.Mappers.Lineup;
using DatabaseLineup = FantasyInformator.Database.Models.Lineup;

namespace FantasyInformator.Database.Tests.Mappers.Lineup;

[TestClass]
public class SlotMapperTests
{
    [TestMethod]
    public void Map_MapsAllFieldsAndChildren()
    {
        var slot = SourceFactory.Create<DatabaseLineup.Slot>();

        var result = SlotMapper.Map(slot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slot, result);
    }

    [TestMethod]
    public void Map_ApplicationModel_MapsAllFieldsAndChildren()
    {
        var model = SourceFactory.Create<DatabaseLineup.Slot>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
