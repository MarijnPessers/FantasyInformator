using FantasyInformator.Database.Mappers.Lineups;
using FantasyInformator.Database.Models.Lineups;

namespace FantasyInformator.Database.Tests.Mappers.Lineups;

[TestClass]
public class LeagueSlotMapperTests
{
    private LeagueSlotMapper _mapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = new LeagueSlotMapper();
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var leagueSlot = SourceFactory.Create<LeagueSlotDb>();

        var result = _mapper.Map(leagueSlot);

        Assert.IsNotNull(result);
        MappedAssert.Equal(leagueSlot, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<LeagueSlotDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
