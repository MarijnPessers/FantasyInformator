using FantasyInformator.Database.Mappers.Lineups;
using FantasyInformator.Database.Models.Lineups;

namespace FantasyInformator.Database.Tests.Mappers.Lineups;

[TestClass]
public class SlotEligibilityMapperTests
{
    private SlotEligibilityMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new SlotEligibilityMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var slotEligibility = SourceFactory.Create<SlotEligibilityDb>();

        var result = _mapper.Map(slotEligibility);

        Assert.IsNotNull(result);
        MappedAssert.Equal(slotEligibility, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<SlotEligibilityDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
