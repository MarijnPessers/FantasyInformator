using FantasyInformator.Database.Mappers.Lineups;
using FantasyInformator.Database.Models.Lineups;

namespace FantasyInformator.Database.Tests.Mappers.Lineups;

[TestClass]
public class SlotMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var slot = SourceFactory.Create<SlotDb>();

        var result = SlotMapper.Map(slot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slot, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<SlotDb>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
