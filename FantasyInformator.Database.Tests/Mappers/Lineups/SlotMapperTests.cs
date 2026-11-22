using FantasyInformator.Database.Mappers.Lineups;
using FantasyInformator.Database.Models.Lineups;

namespace FantasyInformator.Database.Tests.Mappers.Lineups;

[TestClass]
public class SlotMapperTests
{
    private SlotMapper _mapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = new SlotMapper();
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var slot = SourceFactory.Create<SlotDb>();

        var result = _mapper.Map(slot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slot, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<SlotDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
