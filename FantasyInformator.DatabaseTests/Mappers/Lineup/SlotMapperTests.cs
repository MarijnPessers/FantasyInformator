using FantasyInformator.Database.Mappers;
using DatabaseModels = FantasyInformator.Database.Models;

namespace FantasyInformator.DatabaseTests.Mappers;

[TestClass]
public class SlotMapperTests
{
    [TestMethod]
    public void ToModel_MapsAllFieldsAndLinks()
    {
        var slot = SourceFactory.Create<DatabaseModels.Slot>();

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
